using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
[RequiresEntityConversion]
public class ExplosionAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject Prefab;
    public bool hasExplode;
    public float delay;
    public float countdown;
    public bool hasExplosionEntity;

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
            hasExplode = hasExplode,
            delay = delay,
            hasExplosionEntity = hasExplosionEntity,
            countdown = countdown
        };

        var sphereCollider = new Unity.Physics.SphereCollider();
        Debug.Log(Prefab);
        Debug.Log(spawnerData.Prefab);
        dstManager.AddComponentData(entity, spawnerData);
    }
}
