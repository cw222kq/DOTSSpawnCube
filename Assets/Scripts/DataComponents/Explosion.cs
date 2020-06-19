using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Explosion : IComponentData
{
    public Entity spherePrefab;
    [HideInInspector] public float delay;
    [HideInInspector] public float countdown;
    [HideInInspector] public bool hasExplosionEntity;
    [HideInInspector] public float spherePrefabYvalue;
    [HideInInspector] public bool hasExplode;
}
