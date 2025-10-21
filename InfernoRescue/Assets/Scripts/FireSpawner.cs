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

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(firePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            Debug.Log($"Vuur op {spawnPoints[i].name}");
        }
    }
    
}
