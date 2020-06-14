using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent] 
public struct Cube : IComponentData
{
    public Entity cubePrefab;
    public float r;
    public float g;
    public float b;  
}
