using UnityEngine;
using Unity.Entities;
//[GenerateAuthoringComponent] Added class ExplosionAuthoring, if this class is removed uncomment this to genereate the authoring component automatic
public struct Explosion : IComponentData
{
    public bool hasExplode;
    public float delay;
    public float countdown;
    public bool hasExplosionEntity;
    public Entity Prefab;

}
