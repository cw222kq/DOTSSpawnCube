using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent] 
public struct Cube : IComponentData
{
    public Entity cubePrefab;  
}
