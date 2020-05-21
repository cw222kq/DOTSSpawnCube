using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent]
public struct Explosion : IComponentData
{
    public float power;
    public float radius;
    public float upforce;
    public Entity sphere;

}
