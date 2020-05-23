using UnityEngine;
using Unity.Entities;
//[GenerateAuthoringComponent] Added class ExplosionAuthoring, if this class is removed uncomment this to genereate the authoring component automatic
public struct Explosion : IComponentData
{
    public float power;
    public float radius;
    public float upforce;
    public Entity Prefab;

}
