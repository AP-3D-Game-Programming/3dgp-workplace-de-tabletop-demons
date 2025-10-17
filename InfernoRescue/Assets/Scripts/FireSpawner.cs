using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform[] spawnPoints;
    public bool spawnOnStart = true;

    void Start()
    {
        if (spawnOnStart)
        {
            SpawnFire();
        }
    }

    public void SpawnFire()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Geen vuur spawnpoints");
            return;
        }

        int index = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[index];

        Instantiate(firePrefab, point.position, point.rotation);
        Debug.Log($"Vuur op {point.name}");
    }
    
}
