using UnityEngine;
using Unity.Entities;
//[GenerateAuthoringComponent] Added class ExplosionAuthoring, if this class is removed uncomment this to genereate the authoring component automatic
public struct Explosion : IComponentData
{
    public Entity spherePrefab;
    public float spherePrefabYvalue;
    public float delay;
    public float countdown;
    public bool hasExplosionEntity;
    public bool hasExplode;
}
