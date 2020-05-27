using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
[RequiresEntityConversion]
public class ExplosionAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject Prefab;
    public float power;
    public float radius;
    public float upforce;

    // Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(Prefab);
    }

    // Lets you convert the editor data representation to the entity optimal runtime representation
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnerData = new Explosion
        {
            // The referenced prefab will be converted due to DeclareReferencedPrefabs.
            // So here we simply map the game object to an entity reference to that prefab.
            Prefab = conversionSystem.GetPrimaryEntity(Prefab),
            power = power,
            radius = radius,
            upforce = upforce
        };

        var sphereCollider = new Unity.Physics.SphereCollider();
        Debug.Log(Prefab);
        Debug.Log(spawnerData.Prefab);
        dstManager.AddComponentData(entity, spawnerData);
        //dstManager.AddComponentData(entity, sphereCollider);
    }
}
