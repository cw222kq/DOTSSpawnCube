using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateBefore(typeof(SpawnCubeSystem))]
public class MaterialHandler : MonoBehaviour
{
    [SerializeField] public List<Material> cubeMaterials = new List<Material>(); 
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
