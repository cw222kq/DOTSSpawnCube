using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
/*
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
