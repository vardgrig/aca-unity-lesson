using System;
using UnityEngine;

public enum SwipeDirection
{
    Left,
    Right,
    Up,
    Down,
}

public class MobileInput : MonoBehaviour
{
    [SerializeField] private float delta;
    public event Action<SwipeDirection> OnSwipe;

    private bool _isDragging;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _interactableState;

    public void SetInteractableState(bool state)
    {
        _interactableState = state;
    }

    public void UpdateFrame()
    {
        if (!_interactableState)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _endPosition = Input.mousePosition;
        }

        Vector3 dir = _endPosition - _startPosition;


        if (dir.magnitude >= delta && !_isDragging)
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                SwipeComplete(dir.x < 0 ? SwipeDirection.Left : SwipeDirection.Right);
            }
            else
            {
                SwipeComplete(dir.y < 0 ? SwipeDirection.Down : SwipeDirection.Up);
            }


            _startPosition = _endPosition = Vector3.zero;
        }
    }

    private void SwipeComplete(SwipeDirection swipeDirection)
    {
        OnSwipe?.Invoke(swipeDirection);
    }
}