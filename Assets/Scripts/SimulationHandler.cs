using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using UnityEngine.UI;

public class SimulationHandler : MonoBehaviour
{
    // Works it draws the number of cubes that the user inputs in the numberOfCube field.
    // Learned: Never put a script on an entity. If you do the Start and Update method never get executed
    [SerializeField] private int numberOfCubes; // think this is suppose to be hold by the struct cube
    [SerializeField] private GameObject cubePrefab; // think this is suppose to be hold by the struct cube
    [SerializeField] public List<UnityEngine.Material> cubeMaterial = new List<UnityEngine.Material>();
    [SerializeField] public Mesh cubeMesh;
    private Entity cubeEntityPrefab; // think this is suppose to be hold by the struct cube

    private BlobAssetStore blobAssetStore;
    private EntityManager entityManager;
    private int totalCubeCounter = 0;
    private int maxWidth = 10;
    private int maxDeep = 10;
    private int heightCounter = 0;

    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        cubeEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(cubePrefab, settings);
    }

    private void OnDestroy()
    {
        // Cleans up the memory so the next time the simulation is run it does not have the previous blobAssetStore left
        blobAssetStore.Dispose();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
       while(totalCubeCounter<numberOfCubes) {
            // Every level in the cube will consist of max 100 cubes
            for(int i=0; i<maxWidth; i++) {//x width always the same
                for(int j=0; j<maxDeep; j++) {//z deep always the same
                
                    if(totalCubeCounter == numberOfCubes){
                            return;
                    }
                    
                    totalCubeCounter++;
                    SpawnCube(i,heightCounter,j);
                   
                }
            
            }
            // When 100 cubes has been built start the building on the next level
            heightCounter++;

        } 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCube(float x, float y, float z)
    {
        Entity newCubeEntity = entityManager.Instantiate(cubeEntityPrefab);
        //newCubeEntity.UnityEngine.Rendermesh.UnityEngine.Material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        entityManager.AddComponentData(newCubeEntity, new Translation 
        {
            Value = new float3(x, y, z) 
        });

        /*entityManager.AddComponentData(newCubeEntity, new MaterialColor
        {
            Value = new float4(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)) 
        });*/
        entityManager.SetSharedComponentData(newCubeEntity, new RenderMesh 
        {
            material = cubeMaterial[(int)UnityEngine.Random.Range(0, 10)],
            mesh = cubeMesh
        });
        entityManager.AddComponentData(newCubeEntity, new Cube());
        entityManager.AddComponentData(newCubeEntity, new CubeColor());
    } 

    
} 

