using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
[UpdateBefore(typeof(ExplodeCubeSystem))]
[UpdateAfter(typeof(SpawnCubeSystem))]
public class ChangeCubeColorSystem : SystemBase
{
    private Unity.Mathematics.Random random;
 
    protected override void OnCreate() 
    {
        random = new Unity.Mathematics.Random(56);
    }
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((RenderMesh renderMesh, ref CubeColor color) => 
        {
            ChangeColors(renderMesh, color);

        }).Run();

    }
    // Randomize rgb values and set it to the entities material
    private void ChangeColors(RenderMesh renderMesh, CubeColor color) 
    {
        color.r = random.NextFloat(0f, 1f);
        color.g = random.NextFloat(0f, 1f);
        color.b = random.NextFloat(0f, 1f);

        renderMesh.material.color = new Color(color.r, color.g, color.b);
    }
    
}