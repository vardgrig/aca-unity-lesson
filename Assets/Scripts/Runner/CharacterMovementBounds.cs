using System;
using UnityEngine;

public class CharacterMovementBounds : MonoBehaviour
{
    [SerializeField] private Vector3[] bounds;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        if (bounds == null || bounds.Length == 0)
        {
            return;
        }

        for (int i = 0; i < bounds.Length; i++)
        {
            Vector3 position = transform.position;
            position.x = 0f;
            position += bounds[i];
            Gizmos.DrawWireCube(position, Vector3.one);
        }
    }

    public int GetNextIndex(SwipeDirection swipeDirection, int currentIndex)
    {
        int direction = 0;
        if (swipeDirection == SwipeDirection.Left)
        {
            direction = -1;
        }
        else if (swipeDirection == SwipeDirection.Right)
        {
            direction = 1;
        }
        else
        {
            throw new Exception("Unable to move");
        }

        int desiredIndex = currentIndex + direction;
        desiredIndex = Mathf.Clamp(desiredIndex, 0, bounds.Length - 1);
        return desiredIndex;
    }

    public Vector3 GetPosition(int index)
    {
        return bounds[index];
    }

}
