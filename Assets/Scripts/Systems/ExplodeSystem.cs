/*using UnityEngine; ONLY FOR SHOWING OTHERS. WHEN ASKING QUESTIONS.
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Jobs;
using Unity.Transforms;

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
            // Spawn a sphere entity
            Entities
                .WithoutBurst()
                .WithAll<SpawnerTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in Explosion explosion) =>
            {
                var theInstance = ecb.Instantiate(explosion.Prefab);
                ecb.SetComponent(theInstance, new Translation {Value = new float3(SimulationHandler.instance.GetMaxWidth()/2, 0, SimulationHandler.instance.GetMaxWidth()/2)});
                ecb.AddComponent(theInstance, new ExplodeTag());
                ecb.AddComponent(theInstance, new Scale { Value = 1f } );
                hasSphere = true;

            }).Run(); 

        }
        // If sphere exist make it grow
        if(hasSphere)
        {
            Entities
                .WithoutBurst()
                .WithAll<ExplodeTag>()
                .ForEach((Entity entity, ref Translation translation, ref Scale scale, ref PhysicsCollider collider) => 
            { 
                // Increase the size of the sphere and move it upwards so it's placed on the ground while growing
                translation.Value.y = y;
                float increase = 0.05f;
                scale.Value += increase; 
                y += (increase/2);
                        
            }).Run();

        }
    }
} */