using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Jobs;
using Unity.Transforms;
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
            Debug.Log("execute explode");
            //Entities.ForEach((PhysicsCollider collider, ref Explosion explosion) => 
            //{
                //Explode(ecb);
                //Explode(commandBuffer);
                Explode(Buffer);

          //  }).WithoutBurst().Run();
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

        
        // Instanciate a sphere from monobehaviour class SimulationHandler
        //SimulationHandler.instance.SpawnSphere(0, 2, 2);
        //explosion.power = 110f;
        //Debug.Log("explosion power " + explosion.power);
        //Debug.Log("explosion power " + explosion.sphere);
        //Debug.Log("BOOM");
        hasExplode = true;

        // Get highest x-value and highest y-value
        Debug.Log("Deep: " + SimulationHandler.instance.GetMaxDeep());
        Debug.Log("Width: " + SimulationHandler.instance.GetMaxWidth());
        
        // 1. Spawn a sphere entity (TODO: In the middle of the cube structure)
        Entities
            .WithoutBurst()
            .WithAll<SpawnerTag>()
            .WithStructuralChanges()
            .ForEach((Entity entity, int entityInQueryIndex, ref Explosion spawnerFromEntity, in LocalToWorld location) =>
        {
            // with EntityManager
            //var TheInstance = EntityManager.Instantiate(spawnerFromEntity.Prefab);
            //EntityManager.SetComponentData(TheInstance, new Translation {Value = new float3(0, 0, 2)});
            // Removes the spawner entity
            //EntityManager.DestroyEntity(entity);
            Debug.Log("This should only be printed out once");
            var theInstance = ecb.Instantiate(spawnerFromEntity.Prefab);
            ecb.SetComponent(theInstance, new Translation {Value = new float3(0, 0, 2)});
            ecb.DestroyEntity(entity);

        }).Run(); 

        // 2. Make the sphere grow

        // 3. Instantiate the sphere entity in the middle of the cube structure
        

        // Calculate the middle of the structure of cubes

        // Create a sphere entity (without any renderer and collider, this makes the enitity invisable and prevent it to collide with the cubes) 
        // in the middle of the structure of cubes

        // Set the position of the explosion the the sphere entitys position

        // Set force on nearby objects. Defines a shpere on the given position. The method returns an array with all the colliders overlapping with the sphere.

        // Add a explosion force on every object in the stated radius of the sphere object

        // Add force on the colliders that has a collider


        // Remove the sphere entity

    }

}
