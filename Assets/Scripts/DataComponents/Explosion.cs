using UnityEngine;
using Unity.Entities;
 //Added class ExplosionAuthoring, if this class is removed uncomment this to genereate the authoring component automatic
[GenerateAuthoringComponent]
public struct Explosion : IComponentData
{
    public Entity spherePrefab;
    [HideInInspector] public float spherePrefabYvalue;
    [HideInInspector] public float delay;
    [HideInInspector] public float countdown;
    [HideInInspector] public bool hasExplosionEntity;
    [HideInInspector] public bool hasExplode;
}
