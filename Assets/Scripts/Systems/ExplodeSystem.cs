/*using UnityEngine; ONLY USED TO ASK QUESTIONS
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Jobs;
using Unity.Transforms;
using SphereCollider = Unity.Physics.SphereCollider;

[UpdateAfter(typeof(SimulationHandler))]
public class ExplodeSystem : SystemBase
{
    private float countdown;
    private float deelay = 5f;
    private bool hasExplode;
    private float y = 0;
    private bool hasSphere;
    private Entity sphere;  
    BeginInitializationEntityCommandBufferSystem BufferSystem;

    EntityCommandBuffer Buffer;
   
    protected override void OnCreate() 
    {       
        countdown = deelay;
        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    } 
    protected override void OnUpdate() 
    {
        Buffer = BufferSystem.CreateCommandBuffer();
        
        if(!hasExplode) 
        {
            countdown -= Time.DeltaTime;
        }
        if(countdown <= 0f && !hasExplode) 
        {
            Explode(Buffer);
        }     
        
    }

     private void Explode(EntityCommandBuffer ecb)
    {   
        if(!hasSphere)
        {
            Entities
                .WithoutBurst()
                .WithAll<SpawnerTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in Explosion explosion) =>
            {
                Debug.Log("This should only be printed out once");
                var theInstance = ecb.Instantiate(explosion.Prefab);
                ecb.SetComponent(theInstance, new Translation {Value = new float3((SimulationHandler.instance.GetMaxWidth()-1)/2, 0, (SimulationHandler.instance.GetMaxWidth()-1)/2)});
                ecb.AddComponent(theInstance, new ExplodeTag());
                ecb.AddComponent(theInstance, new Scale { Value = 1f } );
                // Adding SphereCollider component
                ecb.AddComponent(theInstance, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = 1f }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                hasSphere = true;
            }).Run(); 
        }
        if(hasSphere)
        {
            Entities
                .WithoutBurst()
                .WithAll<ExplodeTag>()
                .ForEach((Entity entity, ref Translation translation, ref Scale scale, ref PhysicsCollider collider) => 
            { 
                // Increase the size of the sphere and move it upwards so it's placed on the ground while growing
                translation.Value.y = y;
                float increase = 0.15f;
                scale.Value += increase;
                y += (increase/2);
                // Increase the collider by setting the collider radius to the same value as the scale
                ecb.SetComponent(entity, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = scale.Value }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                   
            }).Run();
        }
    }
} */