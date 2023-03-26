using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int numberOfPlatforms = 4;
    [SerializeField] private float distanceBetweenPlatforms = 10f;
    [SerializeField] private Transform playerTransform;

    private Vector3 _lastPosition;

    private readonly Queue<GameObject> platformPool = new();
    private readonly List<GameObject> activePlatforms = new();

    private bool _canSpawn;

    public void Init()
    {
        Vector3 spawnPosition = Vector3.zero;
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject platform = Instantiate(platformPrefab, transform);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform(spawnPosition);
            _lastPosition = spawnPosition;
            spawnPosition += distanceBetweenPlatforms * Vector3.forward;
        }
    }

    public void SetCanSpawnState(bool state)
    {
        _canSpawn = state;
    }

    public void ResetPositions()
    {
        Vector3 spawnPosition = Vector3.zero;
        for (int i = 0; i < activePlatforms.Count; i++)
        {
            activePlatforms[i].transform.position = spawnPosition;
            _lastPosition = spawnPosition;
            spawnPosition += distanceBetweenPlatforms * Vector3.forward;
        }
    }

    public void UpdateFrame()
    {
        if (!_canSpawn)
        {
            return;
        }

        if (!(playerTransform.position.z > activePlatforms[1].transform.position.z))
        {
            return;
        }

        DespawnPlatform(activePlatforms[0]);
        _lastPosition = activePlatforms[^1].transform.position + Vector3.forward * distanceBetweenPlatforms;
        SpawnPlatform(_lastPosition);
    }

    private void SpawnPlatform(Vector3 position)
    {
        GameObject platform = platformPool.Dequeue();
        platform.SetActive(true);
        platform.transform.position = position;
        activePlatforms.Add(platform);
    }

    private void DespawnPlatform(GameObject platform)
    {
        platform.SetActive(false);
        activePlatforms.Remove(platform);
        platformPool.Enqueue(platform);
    }
}