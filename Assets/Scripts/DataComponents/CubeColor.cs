using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent]
public struct CubeColor : IComponentData
{
    public float r;
    public float g;
    public float b;
}