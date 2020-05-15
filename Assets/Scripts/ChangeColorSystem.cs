using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Rendering;
using Unity.Transforms;
public class ChangeColorSystem : SystemBase
{
    private Unity.Mathematics.Random random;
 
    protected override void OnCreate() 
    {
        random = new Unity.Mathematics.Random(56);
    }
    protected override void OnUpdate()
    {
       Entities.ForEach((RenderMesh renderMesh, Cube cube) => 
        {
            renderMesh.material.color = new Color(random.NextFloat(0f, 1f),random.NextFloat(0f, 1f),random.NextFloat(0f, 1f));

        }).WithoutBurst().Run();

    }
    
}