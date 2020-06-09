using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Jobs;
using Unity.Transforms;
using SphereCollider = Unity.Physics.SphereCollider;

[UpdateAfter(typeof(SimulationHandler))]
public class ExplodeCubeSystem : SystemBase
{
    Explosion explosion;
    BeginInitializationEntityCommandBufferSystem BufferSystem;
    EntityCommandBuffer Buffer;

    protected override void OnCreate() 
    {  
        // Set delay of the explosion
        explosion.delay = 5f;
        Debug.Log("explosion.hasExplosionEntity " + explosion.hasExplosionEntity);
        explosion.countdown = explosion.delay;
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    } 
    protected override void OnUpdate() 
    {
        Buffer = BufferSystem.CreateCommandBuffer(); 
        
        if(!explosion.hasExplode) 
        {
            explosion.countdown -= Time.DeltaTime;
        }
        if(explosion.countdown <= 0f && !explosion.hasExplode) 
        {  
            Explode(Buffer);
           
        }
      
    }

    private void Explode(EntityCommandBuffer ecb)
    {   
        // Get reference to system and disable it
        //World.DefaultGameObjectInjectionWorld.GetExistingSystem<ChangeCubeColorSystem>().Enabled = false; makes unity and computer crash???!!!!
        // Get reference to componentData ?????
        // World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>().sphere;
        //Explosion explosion = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>();
        //Explosion explosion = World.DefaultGameObjectInjectionWorld.GetComponentDataFromEntity<Explosion>();
   
        if(!explosion.hasExplosionEntity)
        {
            // 1. Spawn a sphere entity in the middle of the cube structure
            Entities
                .WithoutBurst()
                .WithAll<SpawnerTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in Explosion explosion) =>
            {
                Debug.Log("This should only be printed out once");
                var theInstance = ecb.Instantiate(explosion.spherePrefab);
                ecb.SetComponent(theInstance, new Translation {Value = new float3((SimulationHandler.instance.GetMaxWidth()-1)/2, 0, (SimulationHandler.instance.GetMaxWidth()-1)/2)});
                ecb.AddComponent(theInstance, new ExplodeTag());
                ecb.AddComponent(theInstance, new Scale { Value = 1f } );
                // Adding SphereCollider component
                ecb.AddComponent(theInstance, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = 1f }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                //hasSphere = true;
                

            }).Run(); 

            explosion.hasExplosionEntity = true;
            Debug.Log("explosion.hasExplosionEntity " + explosion.hasExplosionEntity);
        }
        // 2. If sphere exist make it grow
        if(explosion.hasExplosionEntity)
        {
            Entities
                .WithoutBurst()
                .WithAll<ExplodeTag>()
                .ForEach((Entity entity, ref Translation translation, ref Scale scale, ref PhysicsCollider collider) => 
            { 
                // Increase the size of the sphere and move it upwards so it's placed on the ground while growing
                translation.Value.y = explosion.spherePrefabYvalue;
                float increase = 0.15f;
                scale.Value += increase;
                // Increase the collider by setting the collider radius to the same value as the scale value
                ecb.SetComponent(entity, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = scale.Value }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                explosion.spherePrefabYvalue += (increase/2);

                // If the radius of the sphere is equal to or bigger than the maximum width of the cube structure remove the sphere (i.e stop the explosion function)
                if(scale.Value >= SimulationHandler.instance.GetMaxWidth())
                {
                    explosion.hasExplode = true;
                    // Remove the sphere entity
                    ecb.DestroyEntity(entity);      
                }
                
            }).Run();

        }

    }

}
