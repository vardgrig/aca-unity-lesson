using UnityEngine;

namespace DefaultNamespace.Runner
{
    public enum ObstacleType
    {
        Trash,
        Table
    }


    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleType obstacleType;
    }
}