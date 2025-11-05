using UnityEngine;

public class ResidentSpawner : MonoBehaviour
{
    public GameObject[] residentPrefabs;
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
        if (residentPrefabs.Length == 0)
        {
            Debug.LogError("De 'residentPrefabs' array is leeg! Voeg bewoner-prefabs toe in de Inspector.");
            return;
        }
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Geen bewoner spawnpoints");
            return;
        }

        // --- Logica om een willekeurige bewoner te kiezen ---
        // 1. Kies een willekeurige Prefab (uiterlijk)
        int randomPrefabIndex = Random.Range(0, residentPrefabs.Length);
        GameObject prefabToSpawn = residentPrefabs[randomPrefabIndex];

        // 2. Kies een willekeurige SpawnPoint
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawnPoint = spawnPoints[randomSpawnPointIndex];

        // 3. Instantieer de gekozen bewoner op het gekozen punt
        Instantiate(
            prefabToSpawn,
            selectedSpawnPoint.position,
            selectedSpawnPoint.rotation
        );

        Debug.Log($"Bewoner **{prefabToSpawn.name}** gespawnd op **{selectedSpawnPoint.name}**.");

        //for (int i = 0; i < 1; i++)
        //{
        //    int index = Random.Range(0, spawnPoints.Length);
        //    Instantiate(residentPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
        //    Debug.Log($"Bewoner op {spawnPoints[index].name}");
        //}
    }

}
