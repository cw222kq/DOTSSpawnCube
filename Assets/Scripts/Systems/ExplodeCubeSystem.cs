using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Transforms;
using SphereCollider = Unity.Physics.SphereCollider;

[UpdateAfter(typeof(SpawnCubeSystem))]
public class ExplodeCubeSystem : SystemBase
{
    Explosion explosion;
    BeginInitializationEntityCommandBufferSystem BufferSystem;
    EntityCommandBuffer Buffer;
    
    protected override void OnCreate() 
    {   
        // Set delay of the explosion
        explosion.delay = 3f;
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
        // If sphere doesńt exist add it
        if(!explosion.hasExplosionEntity)
        {
            // Spawn a sphere entity in the middle of the cube structure
            Entities
                .WithoutBurst()
                .WithAll<SpawnerTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in Explosion explosion, in Cube cube, in Spawner spawner) =>
            {
                // Instantiate the sphere entity
                var theInstance = ecb.Instantiate(explosion.spherePrefab);
                // Set the position of the sphere entity to be placed in the middle of the cube structure
                //ecb.SetComponent(theInstance, new Translation {Value = new float3((cube.maxWidth-1)/2, 0, (cube.maxDeep-1)/2)});
                ecb.SetComponent(theInstance, new Translation {Value = new float3((spawner.maxWidth-1)/2, 0, (spawner.maxDeep-1)/2)});

                // Add an Spawner on the sphere entity so its possible to get the maxWidth in the entities.foreach at the end of this explode method
                ecb.AddComponent(theInstance, new Spawner { maxWidth = spawner.maxWidth, maxDeep = spawner.maxDeep });


                // Add an ExplodeTag on the sphere entity so its possible to find it with the entities.foreach at the end of this explode method
                ecb.AddComponent(theInstance, new ExplodeTag());
                // Set the radius of the sphere 
                ecb.AddComponent(theInstance, new Scale { Value = 1f } );
                // Adding SphereCollider component and set the radius of it to the same as the sphere radius
                ecb.AddComponent(theInstance, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = 1f }, CollisionFilter.Default, Unity.Physics.Material.Default)});

            }).Run(); 

            // Make sure that this if statement only gets executed once
            explosion.hasExplosionEntity = true;
        }
        
        // If sphere exist make it grow
        if(explosion.hasExplosionEntity)
        {
            Entities
                .WithoutBurst()
                .WithAll<ExplodeTag>()
                .ForEach((Entity entity, ref Translation translation, ref Scale scale, ref PhysicsCollider collider, in Spawner spawner) => 
            { 
                // Increase the size of the sphere and move it upwards so it's placed on the ground while growing
                translation.Value.y = explosion.spherePrefabYvalue;
                float increase = 0.15f; //TODO: Change name to increaseValue
                scale.Value += increase;
                explosion.spherePrefabYvalue += (increase/2);
                // Increase the collider by setting the collider radius to the same value as the scale value
                ecb.SetComponent(entity, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = scale.Value }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                // If the radius of the sphere is equal to or more than the maxWidth of the cube the explosion ends
                if(scale.Value >= (float)spawner.maxWidth)
                {
                    Debug.Log("translation.Value.y " + translation.Value.y);
                    // Set hasExplode to true so this explode method only gets executed once
                    explosion.hasExplode = true;
                    // Remove the sphere entity
                    ecb.DestroyEntity(entity); 
                    // Get a reference to the ChangeCubeColorSystem and disable it before the explosion occurs
                    World.DefaultGameObjectInjectionWorld.GetExistingSystem<ChangeCubeColorSystem>().Enabled = false;   
                }
                
            }).Run();

        }

    }

} 
