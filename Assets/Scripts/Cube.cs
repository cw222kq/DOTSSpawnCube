using Unity.Entities;
using Unity.Mathematics;
[GenerateAuthoringComponent]
public struct Cube : IComponentData
{
    //public GameObject cubePrefab; 
    public Entity cubeEntityPrefab; 
    public float3 position;

    //private float4 color;
    
}
