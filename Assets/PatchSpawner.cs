using UnityEngine;
//Patches are healing items that double as points if unused, this is the spawner for random patches for early testing
public class PatchSpawner : MonoBehaviour
{

    [SerializeField] private GameObject patchPrefab;
    [SerializeField] private BoxCollider groundCollider;
    [SerializeField] private int numPatches = 10;
    void Start() 
    {
        for( int i=0; i< numPatches ; i++)
        {
            Vector3 startPos = RandomPointInBounds(groundCollider.bounds);
            Instantiate(patchPrefab, startPos, Quaternion.identity);
        }
        
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x), 
            1f, 
            Random.Range(bounds.min.z, bounds.max.z)
            );
    }

   
}
