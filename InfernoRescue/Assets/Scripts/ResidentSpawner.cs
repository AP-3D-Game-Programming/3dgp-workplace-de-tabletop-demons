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

        //int index = Random.Range(0, spawnPoints.Length);
        //Transform point = spawnPoints[index];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(residentPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            Debug.Log($"Bewoner op {spawnPoints[i].name}");
        }

        //Instantiate(firePrefab, point.position, point.rotation);
    }

}
