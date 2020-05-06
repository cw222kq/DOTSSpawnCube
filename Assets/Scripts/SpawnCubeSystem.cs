using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;

public class SpawnCubeSystem : JobComponentSystem
{
    public EntityCommandBuffer.Concurrent entityCommandBuffer;

   /* void OnCreate() 
    {
        entityCommandBuffer = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    } */
    protected override JobHandle OnUpdate(JobHandle inputDepts)
    {
       /* Entities.ForEach((ref Cube cube) => {
            //EntityManager.Instantiate(cube.cubeEntityPrefab);
            entityCommandBuffer.CreateEntity(cube.cubeEntityPrefab);
        }).Run(); */

        return default;
    }
}
