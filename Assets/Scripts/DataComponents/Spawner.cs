using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent] 
public struct Spawner : IComponentData
{
    public int numberOfCubes;
    public int width;
    public int deep;
    public int height;
    public int heightCounter;
    public int totalCubeCounter;
    public int maxWidth;
    public int maxDeep;
    public bool spawnedCubes;

}
