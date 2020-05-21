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
    protected override void OnCreate() 
    {   
        //Debug.Log("in OnUpdate");
        countdown = deelay;
        //entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    } 
    protected override void OnUpdate() 
    {
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
                Explode();

          //  }).WithoutBurst().Run();
        }
        

    }

    private void Explode()
    {   
        // Get reference to system and disable it
        //World.DefaultGameObjectInjectionWorld.GetExistingSystem<ChangeCubeColorSystem>().Enabled = false; makes unity and computer crash???!!!!
        // Get reference to componentData ?????
        // World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>().sphere;
        //Explosion explosion = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>();
        //Explosion explosion = World.DefaultGameObjectInjectionWorld.GetComponentDataFromEntity<Explosion>();

        SimulationHandler.instance.SpawnSphere(0, 2, 2);
        //explosion.power = 110f;
        Debug.Log("explosion power " + explosion.power);
        Debug.Log("explosion power " + explosion.sphere);
        Debug.Log("BOOM");
        hasExplode = true;

        // Get highest x-value and highest y-value
        Debug.Log("Deep: " + SimulationHandler.instance.GetMaxDeep());
        Debug.Log("Width: " + SimulationHandler.instance.GetMaxWidth());
        
        // 1. Spawn a sphere entity
        //SimulationHandler.instance.SpawnSphere(0, 2, 2);
        
        /*Entity explosionEntity = EntityManager.Instantiate(SimulationHandler.instance.sphereEntityPrefab);
        EntityManager.AddComponentData(explosionEntity, new Translation 
        {
            Value = new float3(0, 2, 2) 
        });

        EntityManager.SetSharedComponentData(explosionEntity, new RenderMesh 
        {
            material = SimulationHandler.instance.sphereMaterial,
            mesh = SimulationHandler.instance.sphereMesh
        });*/
        //entityManager.Instantiate(World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentdata<Explosion>().sphere);
        //ecb.Playback(EntityManager);
        //ecb.Dispose();

        // 2. Make the shpere grow

        // 3. Instantiate the sphere entity in the middle of the cube structure
        
        
        
        
        
        
        
        //Debug.Log("Deep: " + SimulationHandler.main.GetMaxDeep());
        //Debug.Log("Width: " + SimulationHandler.main.GetMaxWidth());
        
        
        /*Entities.ForEach((PhysicsCollider collider, ref Explosion explosion) => 
        {
            

        }).WithoutBurst().Run();*/

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
