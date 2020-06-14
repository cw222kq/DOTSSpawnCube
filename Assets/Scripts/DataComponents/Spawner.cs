using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent] 
public struct Spawner : IComponentData
{
    public int numberOfCubes;
    public int width;
    public int deep;
    public int height;
    [HideInInspector] public int heightCounter;
    [HideInInspector] public int totalCubeCounter;
    [HideInInspector] public int maxWidth;
    [HideInInspector] public int maxDeep;
    [HideInInspector] public bool spawnedCubes;

}
