using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject prefab;

    public void SpawnPrefab()
    {
        Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
    }
}
