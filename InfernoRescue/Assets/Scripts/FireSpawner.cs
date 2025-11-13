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

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Gebruik de 'spawnPoint' direct
            Instantiate(firePrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Vuur op {spawnPoint.name}");
        }
    }
    
}
