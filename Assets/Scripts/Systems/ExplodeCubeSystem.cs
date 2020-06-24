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
    private Explosion explosion;
    private BeginInitializationEntityCommandBufferSystem BufferSystem;
    private EntityCommandBuffer Buffer;
    
    protected override void OnCreate() 
    {   
        // Set the delay of the explosion
        explosion.delay = 3f;
        explosion.countdown = explosion.delay;

        // Initialize the EntityCommandBuffer
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    } 

    protected override void OnUpdate() 
    {
        // Create EntityCommandBuffer so it's possible to add components to entities as well as modify components on entities
        Buffer = BufferSystem.CreateCommandBuffer(); 
        
        explosion.countdown -= Time.DeltaTime;
       
        // If the countdown is 0 or less the Explode method will be called
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
            // This code will run on each entity with a SpawnerTag (i.e only the Spawner entity in the scene). 
            Entities
                .WithoutBurst()
                .WithAll<SpawnerTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in Explosion explosion, in Spawner spawner) =>
            {
                // Instantiate the sphere entity
                var theInstance = ecb.Instantiate(explosion.spherePrefab);

                // Set the position of the sphere entity to be placed in the middle of the cube structure
                ecb.SetComponent(theInstance, new Translation {Value = new float3(((float)spawner.maxWidth-1)/2, 0, ((float)spawner.maxDeep-1)/2)});

                // Add a Spawner on the sphere entity so its possible to get the maxWidth in the entities.foreach at the end of this explode method
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
            // This code will run on each entity with a ExplodeTag (i.e only the Sphere entity in the scene).
            Entities
                .WithoutBurst()
                .WithAll<ExplodeTag>()
                .ForEach((Entity entity, ref Translation translation, ref Scale scale, ref PhysicsCollider collider, in Spawner spawner) => 
            { 
                // Increase the size of the sphere and move it upwards so it will result in an up force of the explosion
                translation.Value.y = explosion.spherePrefabYvalue;
                
                // Set the value that the scale will increase within every round in the loop
                float increaseValue = 0.15f;
                scale.Value += increaseValue;

                // Set the value that the y position of the sphere will increase within every round in the loop
                explosion.spherePrefabYvalue += increaseValue;

                // Increase the collider by setting the collider radius to the same value as the scale value (this makes the collider twice as big as the sphere).
                ecb.SetComponent(entity, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = scale.Value }, CollisionFilter.Default, Unity.Physics.Material.Default)});
        
                // If the radius of the sphere is equal to or more than the maxWidth of the cube the explosion ends
                if(scale.Value >= (float)spawner.maxWidth)
                {
                    // Set hasExplode to true so this explode method only gets executed once
                    explosion.hasExplode = true;

                    // Remove the sphere entity
                    ecb.DestroyEntity(entity); 

                    // Get a reference to the ChangeCubeColorSystem and disable it before the explosion fully occurs
                    World.DefaultGameObjectInjectionWorld.GetExistingSystem<ChangeCubeColorSystem>().Enabled = false;   
                }
                
            }).Run();

        }

    }

} 
