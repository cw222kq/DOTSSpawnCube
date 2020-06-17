using UnityEngine;
using Unity.Entities;

[UpdateBefore(typeof(SpawnCubeSystem))]
public class MeshHandler : MonoBehaviour
{
    [SerializeField] public Mesh cubeMesh;
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
