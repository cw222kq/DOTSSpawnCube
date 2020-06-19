using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

[UpdateBefore(typeof(ExplodeCubeSystem))]
[UpdateAfter(typeof(SpawnCubeSystem))]
public class ChangeCubeColorSystem : SystemBase
{
    protected override void OnUpdate()
    {
        ChangeColors();
    }
   
    // Randomize rgb values and set it to the entities material
    private void ChangeColors()
    {
        // This code will run on each entity with a RenderMesh component and a Cube component (i.e on every cube in the scene). 
        Entities
            .WithoutBurst()
            .ForEach((RenderMesh renderMesh, ref Cube cubeColor) => 
        {
            cubeColor.r = Random.Range(0f, 1f);
            cubeColor.g = Random.Range(0f, 1f);
            cubeColor.b = Random.Range(0f, 1f);

            renderMesh.material.color = new Color(cubeColor.r, cubeColor.g, cubeColor.b);

        }).Run();
    }
    
}