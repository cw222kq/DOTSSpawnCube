using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent] 
public struct Cube : IComponentData
{
    public Entity cubePrefab;
    [HideInInspector] public float r;
    [HideInInspector] public float g;
    [HideInInspector] public float b;  

}
