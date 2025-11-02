using UnityEngine;

public class ResidentSpawner : MonoBehaviour
{
    public GameObject residentPrefab;
    public Transform[] spawnPoints;
    public bool spawnOnStart = true;

    void Start()
    {
        if (spawnOnStart)
        {
            SpawnResident();
        }
    }

    public void SpawnResident()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Geen bewoner spawnpoints");
            return;
        }

        for (int i = 0; i < 1; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(residentPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
            Debug.Log($"Bewoner op {spawnPoints[index].name}");
        }
    }

}
