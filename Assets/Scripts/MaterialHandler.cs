using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

// Since struct is a value type (contain their data) it can only store other value types since Material is a reference type (store references to their data) 
// a class must be used for storage
[UpdateBefore(typeof(SpawnCubeSystem))]
public class MaterialHandler : MonoBehaviour
{
    [SerializeField] public List<Material> cubeMaterials = new List<Material>(); 
    
    // Singleton
    public static MaterialHandler instance;
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
