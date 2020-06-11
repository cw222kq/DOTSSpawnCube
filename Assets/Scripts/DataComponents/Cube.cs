using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent] 
public struct Cube : IComponentData
{
    public Entity cubePrefab;
    public int numberOfCubes;
    public int width;
    public int deep;
    public int height;
    public int heightCounter; //= 0;
    public int totalCubeCounter; //= 0;
    public int maxWidth; //= 10;
    public int maxDeep; //= 10;
     
}
