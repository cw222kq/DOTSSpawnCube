using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
public class ChangeColorSystem : SystemBase
{
    private Unity.Mathematics.Random random;
 
    protected override void OnCreate() 
    {
        random = new Unity.Mathematics.Random(56);
    }
    protected override void OnUpdate()
    {
       Entities.ForEach((RenderMesh renderMesh, ref CubeColor color) => 
        {
            color.r = random.NextFloat(0f, 1f);
            color.g = random.NextFloat(0f, 1f);
            color.b = random.NextFloat(0f, 1f);

            renderMesh.material.color = new Color(color.r, color.g, color.b);

        }).WithoutBurst().Run();

    }
    
}