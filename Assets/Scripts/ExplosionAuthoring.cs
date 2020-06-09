using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
[RequiresEntityConversion]
public class ExplosionAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject spherePrefab;
    public float spherePrefabYvalue;
    public float delay;
    public float countdown;
    public bool hasExplosionEntity;
    public bool hasExplode;

    // Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(spherePrefab);
    }

    // Lets you convert the editor data representation to the entity optimal runtime representation
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnerData = new Explosion
        {
            // The referenced prefab will be converted due to DeclareReferencedPrefabs.
            // So here we simply map the game object to an entity reference to that prefab.
            spherePrefab = conversionSystem.GetPrimaryEntity(spherePrefab),
            spherePrefabYvalue = spherePrefabYvalue,
            delay = delay,
            countdown = countdown,
            hasExplosionEntity = hasExplosionEntity,
            hasExplode = hasExplode
        };
        // Add spawnerData at the entity
        dstManager.AddComponentData(entity, spawnerData);
    }
}
