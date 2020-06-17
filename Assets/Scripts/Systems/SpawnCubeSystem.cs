using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Transforms;

[UpdateBefore(typeof(ExplodeCubeSystem))]
public class SpawnCubeSystem : SystemBase
{
    BeginInitializationEntityCommandBufferSystem BufferSystem;
    EntityCommandBuffer Buffer;
    Spawner spawner;

     protected override void OnCreate() 
    {
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate() 
    {
        Buffer = BufferSystem.CreateCommandBuffer(); 
    
        if (!spawner.spawnedCubes)
        {
            ProcessUserInput();
            SpawnCube(Buffer); 
        }
       
    }

    private void SpawnCube(EntityCommandBuffer ecb)
    {
        // Spawn a cube entity
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Spawner spawner, in Cube cube) =>
        {
       
            while(spawner.totalCubeCounter < spawner.numberOfCubes) 
            {
                for(int i=0; i<spawner.maxWidth; i++) {//x width always the same
                    for(int j=0; j<spawner.maxDeep; j++) {//z deep always the same
                
                        if(spawner.totalCubeCounter == spawner.numberOfCubes){
                            return;
                        }
                        spawner.totalCubeCounter++;
                        var theInstance = ecb.Instantiate(cube.cubePrefab);
                        // Set the x, y and z value 
                        ecb.SetComponent(theInstance, new Translation { Value = new float3(i, spawner.heightCounter, j) });
                        // Add component Cube so the ChangeCubeColorSystem will work on the cubes
                        ecb.AddComponent(theInstance, new Cube());
                        // Randomize the material so the cubes will have different materials and can have different colors
                        ecb.SetSharedComponent(theInstance, new RenderMesh 
                        { 
                            material = SimulationHandler.instance.cubeMaterials[(int)UnityEngine.Random.Range(0, 10)],
                            mesh = SimulationHandler.instance.cubeMesh 
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
    
    private void ProcessUserInput()
    {
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Spawner spawner) =>
        {
            // Default value if the user only input the numberOfCubes
            spawner.maxDeep = 10;
            spawner.maxWidth = 10;

            // If the user inputs width, deep and hight recalculate the maxWidth, maxDeep and the numberOfCubes 
            if (spawner.width > 0 && spawner.deep > 0 && spawner.height > 0) 
            {
                spawner.maxWidth = spawner.width;
                spawner.maxDeep = spawner.deep;
                spawner.numberOfCubes = (spawner.width*spawner.deep)*spawner.height; 
            }
        
        }).Run();      
    }  
} 
