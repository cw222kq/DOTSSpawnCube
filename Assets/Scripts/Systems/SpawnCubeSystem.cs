using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Transforms;

[UpdateBefore(typeof(ExplodeCubeSystem))]
public class SpawnCubeSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem BufferSystem;
    private EntityCommandBuffer Buffer;
    private Spawner spawner;

    // Initialize the EntityCommandBuffer
     protected override void OnCreate() 
    {
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate() 
    {
        // Create EntityCommandBuffer so it's possible to add components to entities as well as modify components on entities
        Buffer = BufferSystem.CreateCommandBuffer(); 
    
        // If no cubes have been spawned the SpawnCube method is called after the user input has been modified
        if (!spawner.spawnedCubes)
        {
            ProcessUserInput();
            SpawnCubes(Buffer); 
        }
       
    }

    private void SpawnCubes(EntityCommandBuffer ecb)
    {
        // This code will run on each entity with a SpawnerTag (i.e only the Spawner entity in the scene). 
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Spawner spawner, in Cube cube) =>
        {
            // As long as the number of spawned cubes in the scene is smaller than numberOfCubes the spawner will keep on spawning cubes
            while(spawner.totalCubeCounter < spawner.numberOfCubes) 
            {
                for(int i=0; i<spawner.maxWidth; i++) { // x width default value is 10
                    for(int j=0; j<spawner.maxDeep; j++) { // z deep default value is 10
                
                        // This will end the while loop
                        if(spawner.totalCubeCounter == spawner.numberOfCubes){
                            return;
                        }

                        spawner.totalCubeCounter++;

                        // Instantiate Entity 
                        var theInstance = ecb.Instantiate(cube.cubePrefab);

                        // Set the x, y and z value for the entity
                        ecb.SetComponent(theInstance, new Translation { Value = new float3(i, spawner.heightCounter, j) });

                        // Add the Cube component so the ChangeCubeColorSystem will work on the cubes
                        ecb.AddComponent(theInstance, new Cube());

                        // Randomize the material so the cubes will have different materials and can have different colors
                        ecb.SetSharedComponent(theInstance, new RenderMesh 
                        { 
                            material = MaterialHandler.instance.cubeMaterials[(int)UnityEngine.Random.Range(0, 10)],
                            mesh = MeshHandler.instance.cubeMesh
                        });
                    }   
                }
                
                // When the first level of cubes (maxWidth*maxDeep) has been built start the building on the next level (i.e adding +1 to the height (y value))
                spawner.heightCounter++;
            }

        }).Run();  

        //Set spawedCubes to true so the SpawnCube method only gets executed once
        spawner.spawnedCubes = true; 
    }

    // Checks the user input from the inspector. If width, deep and height is set to values those values will be used to set the numberOfCubes
    // If width, deep or height are set to zero the building of cubes will consist of the value set in the numberOfCubes field
    // Every complete level will consist of 100 cubes (default value)
    private void ProcessUserInput()
    {
        Entities
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, ref Spawner spawner) => 
        {
            // Default value if the user only input the numberOfCubes
            spawner.maxDeep = 10;
            spawner.maxWidth = 10;

            // If the user inputs width, deep or hight recalculate the maxWidth, maxDeep and the numberOfCubes 
            if (spawner.width > 0 && spawner.deep > 0 && spawner.height > 0) 
            {
                spawner.maxWidth = spawner.width;
                spawner.maxDeep = spawner.deep;
                spawner.numberOfCubes = (spawner.width*spawner.deep)*spawner.height; 
            }
        
        }).Run();      
    }  
} 
