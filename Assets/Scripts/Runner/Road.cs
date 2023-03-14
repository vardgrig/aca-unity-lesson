using System;
using System.Collections.Generic;
using DefaultNamespace.Runner;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class ObstacleSpawnPoints
{
    [SerializeField] private ObstacleType obstacleType;
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Transform[] spawnPoint;


    public ObstacleType ObstacleType => obstacleType;
    public Obstacle ObstaclePrefab => obstaclePrefab;

    public int Count => spawnPoint.Length;

    public Transform GetTransform(int index)
    {
        if (index < 0 || index >= spawnPoint.Length)
        {
            Debug.LogError("Index out of range");
            return null;
        }

        Transform point = spawnPoint[index];
        return point;
    }
}

public class Road : MonoBehaviour
{
    [SerializeField] private ObstacleSpawnPoints[] _obstacleSpawnPoints;
    [SerializeField] [Range(0, 1)] private float fillPercent;

    private readonly List<Obstacle> _obstacles = new List<Obstacle>();
    private void Start()
    {
        SetupRoad();
    }
    
    private void SetupRoad()
    {
        int totalObstaclePointCount = 0;
        for (int i = 0; i < _obstacleSpawnPoints.Length; i++)
        {
            totalObstaclePointCount += _obstacleSpawnPoints[i].Count;
        }

        int actualObstacleCount = Mathf.RoundToInt(totalObstaclePointCount * fillPercent);
        int totalObstacleCount = Enum.GetNames(typeof(ObstacleType)).Length;
        for (int i = 0; i < actualObstacleCount; i++)
        {
            ObstacleType obstacleType = (ObstacleType)Random.Range(0, totalObstacleCount);
            CreateObstacle(obstacleType);
        }
    }

    private void CreateObstacle(ObstacleType obstacleType)
    {
        ObstacleSpawnPoints container = null;
        for (int i = 0; i < _obstacleSpawnPoints.Length; i++)
        {
            if (_obstacleSpawnPoints[i].ObstacleType == obstacleType)
            {
                container = _obstacleSpawnPoints[i];
                break;
            }
        }

        if (container == null)
        {
            Debug.LogError($"Cannot find obstacle container with type: {obstacleType}");
            return;
        }

        int randomPointIndex = Random.Range(0, container.Count);
        Obstacle obstacle = Instantiate(container.ObstaclePrefab, container.GetTransform(randomPointIndex));
        _obstacles.Add(obstacle);
    }
}