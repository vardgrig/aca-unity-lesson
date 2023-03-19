using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 4;
    public float distanceBetweenPlatforms = 10f;
    public Transform playerTransform;
    Vector3 lastPosition;

    private Queue<GameObject> platformPool = new();
    private List<GameObject> activePlatforms = new();

    void Start()
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
            lastPosition = spawnPosition;
            spawnPosition += distanceBetweenPlatforms * Vector3.forward;
        }
    }

    void Update()
    {
        if (playerTransform.position.z > activePlatforms[1].transform.position.z)
        {
            DespawnPlatform(activePlatforms[0]);
            lastPosition = activePlatforms[^1].transform.position + Vector3.forward * distanceBetweenPlatforms;
            SpawnPlatform(lastPosition);
        }
    }

    void SpawnPlatform(Vector3 position)
    {
        GameObject platform = platformPool.Dequeue();
        platform.SetActive(true);
        platform.transform.position = position;
        activePlatforms.Add(platform);
    }

    void DespawnPlatform(GameObject platform)
    {
        platform.SetActive(false);
        activePlatforms.Remove(platform);
        platformPool.Enqueue(platform);
    }
}