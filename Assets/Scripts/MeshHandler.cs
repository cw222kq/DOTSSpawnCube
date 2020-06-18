using UnityEngine;
using Unity.Entities;

// Since struct is a value type (contain their data) it can only store other value types since Mesh is a reference type (store references to their data) 
// a class must be used for storage
[UpdateBefore(typeof(SpawnCubeSystem))]
public class MeshHandler : MonoBehaviour
{
    [SerializeField] public Mesh cubeMesh;

    // Singleton
    public static MeshHandler instance;
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
