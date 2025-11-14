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
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomPrefabIndex = Random.Range(0, residentPrefabs.Length);
            GameObject prefabToSpawn = residentPrefabs[randomPrefabIndex];

            Instantiate(
                prefabToSpawn,
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
        if (LevelManager.Instance != null)
        {
            // Het totaal is gelijk aan het aantal spawnpoints
            LevelManager.Instance.SetTotalResidents(spawnPoints.Length);
        }
        else
        {
            Debug.LogError("LevelManager.Instance niet gevonden!");
        }
    }

}
