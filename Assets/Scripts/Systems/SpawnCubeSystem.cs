using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Transforms;
[UpdateAfter(typeof(SimulationHandler))]
public class SpawnCubeSystem : SystemBase
{
    //[SerializeField] private List<UnityEngine.Material> cubeMaterial = new List<UnityEngine.Material>(); Investigate how to do with this ???
    //[SerializeField] private Mesh cubeMesh; Investigate how to do with this ???
    BeginInitializationEntityCommandBufferSystem BufferSystem;
    EntityCommandBuffer Buffer;
     protected override void OnCreate() 
    {
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate() 
    {
        Buffer = BufferSystem.CreateCommandBuffer(); 
        if (!SimulationHandler.instance.spawnedCubes)
        {
            ProcessUserInput();
            SpawnCube(Buffer); 
            //Set spawedCubes to true so this method only get called once
            //spawnedCubes = true;
        }
       
    }
    private void SpawnCube(EntityCommandBuffer ecb)
    {
        // Spawn a cube entity
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Cube cube) =>
        {
       
            while(cube.totalCubeCounter < cube.numberOfCubes) 
            {
                for(int i=0; i<cube.maxWidth; i++) {//x width always the same
                    for(int j=0; j<cube.maxDeep; j++) {//z deep always the same
                
                        if(cube.totalCubeCounter == cube.numberOfCubes){
                            return;
                        }
                        cube.totalCubeCounter++;
                        var theInstance = ecb.Instantiate(cube.cubePrefab);
                        // Set the x, y and z value 
                        ecb.SetComponent(theInstance, new Translation { Value = new float3(i, cube.heightCounter, j) });
                        // Randomize the material
                        ecb.SetSharedComponent(theInstance, new RenderMesh 
                        { 
                            material = SimulationHandler.instance.cubeMaterial[(int)UnityEngine.Random.Range(0, 10)],
                            mesh = SimulationHandler.instance.cubeMesh 
                        });
                    }   
                }
                // When maxWidth*maxDeep has been built start the building on the next level
                cube.heightCounter++;
            }

        }).Run();  

        //Set spawedCubes to true so this method only get called once
        SimulationHandler.instance.spawnedCubes = true;   
    }
    private void ProcessUserInput() //THINK THAT THIS SHOULD BE STORED IN THE SIMULATIONHANDLER
    {
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Cube cube) =>
        {
            // Default value if the user only input the numberOfCubes
            cube.maxDeep = 10;
            cube.maxWidth = 10;

            // If the user inputs width, deep and hight recalculate the maxWidth, maxDeep and the numberOfCubes 
            if (cube.width > 0 && cube.deep > 0 && cube.height > 0) 
            {
                cube.maxWidth = cube.width;
                cube.maxDeep = cube.deep;
                cube.numberOfCubes = (cube.width*cube.deep)*cube.height; 
            }
        
        }).Run();      
    }  
}
