using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Jobs;
using Unity.Transforms;
using SphereCollider = Unity.Physics.SphereCollider;
//using Unity.Physics.Systems;

[UpdateAfter(typeof(SimulationHandler))]
public class ExplodeCubeSystem : SystemBase
{
    private float countdown;
    private float deelay = 5f;
    private bool hasExplode;
    //[Inject] private ChangeCubeColorSystem changeCubeColorSystem;
    //EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
    //private EntityManager entityManager;
    private float y = 0;
    private float physicsValue = 0.8f;
    private bool hasSphere;
    private Entity sphere;
    private Explosion explosion; // Works if setting the value first but does not work if getting the entity from explosion. The entity from explosion is null (?)
    
    // THIS WORKS TOGETHER WITH EXPLOSION AND EXPLOSIONAUTHORING
    //BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    BeginInitializationEntityCommandBufferSystem BufferSystem;

    EntityCommandBuffer Buffer;

    //EntityCommandBuffer ecb;
    
    protected override void OnCreate() 
    {   
        //Debug.Log("in OnUpdate");
        countdown = deelay;
        //entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // THIS WORKS TOGETHER WITH EXPLOSION AND EXPLOSIONAUTHORING
        //m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>(); //WORKS WITH WORKER THREADS


        //ecb = new EntityCommandBuffer();

        BufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        //Buffer = BufferSystem.CreateCommandBuffer();
        

    } 
    protected override void OnUpdate() 
    {
        // THIS WORKS TOGETHER WITH EXPLOSION AND EXPLOSIONAUTHORING
        //EntityCommandBuffer.Concurrent commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(); //WORKS WITH WORKER THREADS
        Buffer = BufferSystem.CreateCommandBuffer();
        

        //Debug.Log("in OnUpdate");
        if(!hasExplode) 
        {
            countdown -= Time.DeltaTime;
        }
        if(countdown <= 0f && !hasExplode) 
        {
            //Debug.Log("execute explode");
            //Entities.ForEach((PhysicsCollider collider, ref Explosion explosion) => 
            //{
                
                Explode(Buffer);
                
        
                
          //  }).WithoutBurst().Run();
        }
        /*if(hasExplode) {

            Entities
                .WithoutBurst()
                .WithAll<ExplodeTag>()
                .ForEach((Entity entity, ref Translation translation) => 
            {
                translation.Value = new float3(5, 5, 5); 
                

            }).Run();



        }*/

        
        
    }

    private void Explode(EntityCommandBuffer ecb)
    {   

        // Get reference to system and disable it
        //World.DefaultGameObjectInjectionWorld.GetExistingSystem<ChangeCubeColorSystem>().Enabled = false; makes unity and computer crash???!!!!
        // Get reference to componentData ?????
        // World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>().sphere;
        //Explosion explosion = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>();
        //Explosion explosion = World.DefaultGameObjectInjectionWorld.GetComponentDataFromEntity<Explosion>();
   
        if(!hasSphere)
        {
            // 1. Spawn a sphere entity in the middle of the cube structure
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
                ecb.AddComponent(theInstance, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = physicsValue }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                hasSphere = true;

            }).Run(); 

        }
        // 2. If sphere exist make it grow
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
                ecb.SetComponent(entity, new PhysicsCollider { Value = SphereCollider.Create(new SphereGeometry { Center = float3.zero, Radius = scale.Value }, CollisionFilter.Default, Unity.Physics.Material.Default)});
                y += (increase/2);
                
            }).Run();

        }

        // Remove the sphere entity

    }

}
