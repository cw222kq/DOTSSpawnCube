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
        // The ChangeColors method is run on every entity that holds both the RenderMesh and the Cube component
        Entities
            .WithoutBurst()
            .ForEach((RenderMesh renderMesh, ref Cube cubeColor) => 
        {
            ChangeColors(renderMesh, cubeColor);

        }).Run();

    }
    // Randomize rgb values and set it to the entities material
    private void ChangeColors(RenderMesh renderMesh, Cube cubeColor) // TODO: Change and set the Entities foreach in the method
    {
        cubeColor.r = random.NextFloat(0f, 1f);
        cubeColor.g = random.NextFloat(0f, 1f);
        cubeColor.b = random.NextFloat(0f, 1f);

        renderMesh.material.color = new Color(cubeColor.r, cubeColor.g, cubeColor.b);
    }
    
}