using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
[UpdateBefore(typeof(SpawnCubeSystem))]
public class SimulationHandler : MonoBehaviour
{
    [SerializeField] public List<Material> cubeMaterial = new List<Material>(); 
    [SerializeField] public Mesh cubeMesh;
    public bool spawnedCubes; // TODO: Move this???
  
    // Singleton
    public static SimulationHandler instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
} 

