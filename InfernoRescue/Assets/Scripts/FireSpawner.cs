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

        for (int i = 0; i < 2; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(firePrefab, spawnPoints[index].position, spawnPoints[index].rotation);
            Debug.Log($"Vuur op {spawnPoints[index].name}");
        }
    }
    
}
