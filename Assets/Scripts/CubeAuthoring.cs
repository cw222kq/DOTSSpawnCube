/*using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
[RequiresEntityConversion]
public class CubeAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
   public GameObject cubePrefab;
    public int numberOfCubes;
    public int width;
    public int deep;
    public int height;
    public int heightCounter; //= 0;
    public int totalCubeCounter; //= 0;
    public int maxWidth; //= 10;
    public int maxDeep; //= 10;

    // Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(cubePrefab);
    }

    // Lets you convert the editor data representation to the entity optimal runtime representation
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var cubeData = new Cube
        {
            // The referenced prefab will be converted due to DeclareReferencedPrefabs.
            // So here we simply map the game object to an entity reference to that prefab.
            cubePrefab = conversionSystem.GetPrimaryEntity(cubePrefab),
            numberOfCubes = numberOfCubes,
            width = width,
            deep = deep,
            height = height,
            heightCounter = heightCounter, //set to default value 0;
            totalCubeCounter = totalCubeCounter,  //set to default value 0;
            maxWidth = maxWidth, //set to default value 10;
            maxDeep = maxDeep //set to default value 0;
        };
        // Add spawnerData at the entity
        dstManager.AddComponentData(entity, cubeData);
    }
}*/
