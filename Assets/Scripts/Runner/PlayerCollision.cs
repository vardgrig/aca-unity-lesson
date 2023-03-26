using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event Action<IObstacle> OnCollisionObstacle;

    private void OnCollisionEnter(Collision collision)
    {
        IEntity entity = collision.collider.GetComponent<IEntity>();

        switch (entity)
        {
            case ICollectable collectable:
                HandleCollectable(collectable);
                break;
            case IObstacle obstacle:
                HandleObstacle(obstacle);
                break;
        }
    }

    private void HandleObstacle(IObstacle obstacle)
    {
        OnCollisionObstacle?.Invoke(obstacle);
    }

    private void HandleCollectable(ICollectable collectable)
    {
    }
}