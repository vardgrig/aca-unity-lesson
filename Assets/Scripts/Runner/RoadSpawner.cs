using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 4;
    public float distanceBetweenPlatforms = 10f;
    public Transform playerTransform;
    Vector3 lastPosition;
    bool isGameStarted = false;

    private Queue<GameObject> platformPool = new();
    private List<GameObject> activePlatforms = new();


    void Start()
    {
        GameManager.instance.OnGameStarted += OnGameStarted;
        GameManager.instance.OnGameOver += OnGameOver;
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

    void OnGameOver()
    {
        isGameStarted = false;
    }
    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(0.2f);
        var spawnPosition = Vector3.zero;
        foreach (var road in activePlatforms)
        {
            road.transform.position = spawnPosition;
            lastPosition = spawnPosition;
            spawnPosition += Vector3.forward * distanceBetweenPlatforms;
        }
    }

    void Update()
    {
        if (isGameStarted)
        {
            if (playerTransform.position.z > activePlatforms[1].transform.position.z)
            {
                DespawnPlatform(activePlatforms[0]);
                lastPosition = activePlatforms[^1].transform.position + Vector3.forward * distanceBetweenPlatforms;
                SpawnPlatform(lastPosition);
            }
        }
    }

    void OnGameStarted()
    {
        StartCoroutine(ResetPosition());
        isGameStarted = true;
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