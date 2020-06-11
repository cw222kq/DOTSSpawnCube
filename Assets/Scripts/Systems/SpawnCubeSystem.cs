using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Transforms;
/* OLD
[AlwaysSynchronizeSystem] 
public class SpawnCubeSystem : JobComponentSystem
{
    //public EntityCommandBuffer.Concurrent entityCommandBuffer;

     void OnCreate() 
    {   //Cube cube = new Cube();
        [Inject] Cube cube;
        //entityCommandBuffer = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        cube.cubeEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(cube.cubePrefab, settings);
    } 
    protected override JobHandle OnUpdate(JobHandle inputDepts)
    {
        Entities.ForEach((ref Cube cube) => {
            EntityManager.Instantiate(cube.cubeEntityPrefab);
            //EntityManager.Instantiate(cube.cubePrefab);
            //entityCommandBuffer.CreateEntity(cube.cubeEntityPrefab);
        }).WithoutBurst().Run(); 

        return default;
    }
} */
/*
public class SpawnCubeSystem : SystemBase
{
    //public EntityCommandBuffer.Concurrent entityCommandBuffer;

     void OnCreate() 
    {  //Cube cube = new Cube();
        [Inject] Cube cube;
        //entityCommandBuffer = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        cube.cubeEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(cube.cubePrefab, settings);
    } 
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Cube cube) => {
            EntityManager.Instantiate(cube.cubeEntityPrefab);
            //EntityManager.Instantiate(cube.cubePrefab);
            //entityCommandBuffer.CreateEntity(cube.cubeEntityPrefab);
        }).WithoutBurst().Run(); 

    }
} */
[UpdateAfter(typeof(SimulationHandler))]
public class SpawnCubeSystem : SystemBase
{
    //[SerializeField] private List<UnityEngine.Material> cubeMaterial = new List<UnityEngine.Material>(); Investigate how to do with this ???
    //[SerializeField] private Mesh cubeMesh; Investigate how to do with this ???
    BeginInitializationEntityCommandBufferSystem BufferSystem;
    EntityCommandBuffer Buffer;
    bool spawnedCube;
     protected override void OnCreate() 
    {
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate() 
    {
        Buffer = BufferSystem.CreateCommandBuffer(); 
        if (!spawnedCube)
        {
            ProcessUserInput();
            SpawnCube(Buffer);
            spawnedCube = true;
        }
       
    }
    private void SpawnCube(EntityCommandBuffer ecb)
    {
        // 1. Spawn a cube entity
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Cube cube) =>
        {
        Debug.Log("THIS should only be printed out once");
 
        while(cube.totalCubeCounter < cube.numberOfCubes) 
        {
            for(int i=0; i<cube.maxWidth; i++) {//x width always the same
                for(int j=0; j<cube.maxDeep; j++) {//z deep always the same
                
                    if(cube.totalCubeCounter == cube.numberOfCubes){
                            return;
                    }
                    cube.totalCubeCounter++;
                    var theInstance = ecb.Instantiate(cube.cubePrefab);
                    ecb.SetComponent(theInstance, new Translation { Value = new float3(i, cube.heightCounter, j) });
                }
            
            }
            // When maxWidth*maxDeep has been built start the building on the next level
            cube.heightCounter++;
        }
        
        }).Run(); 
    }
    private void ProcessUserInput() //THINK THAT THIS SHOULD BE STORED IN THE SIMULATIONHANDLER
    {
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Cube cube) =>
        {
            Debug.Log("This should only be printed out once");

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
