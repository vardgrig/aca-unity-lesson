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
            activePlatforms.Add(platform);
        }

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform(spawnPosition);
            lastPosition = spawnPosition;
            spawnPosition += new Vector3(0, 0, distanceBetweenPlatforms);
        }
    }

    void Update()
    {
        if (playerTransform.position.z > activePlatforms[1].transform.position.z)
        {
            DespawnPlatform(activePlatforms[0]);
            lastPosition = activePlatforms[^1].transform.position + new Vector3(0, 0, distanceBetweenPlatforms);
            SpawnPlatform(lastPosition);
        }
    }

    void SpawnPlatform(Vector3 position)
    {
        GameObject platform = platformPool.Dequeue();
        platform.SetActive(true);
        platform.transform.position = position;
        activePlatforms.Add(platform);
        platformPool.Enqueue(activePlatforms[^1]);
    }

    void DespawnPlatform(GameObject platform)
    {
        platform.SetActive(false);
        platformPool.Enqueue(platform);
        activePlatforms.Remove(platform);
    }
}