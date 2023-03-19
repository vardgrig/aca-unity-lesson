using System;
using UnityEngine;

namespace DefaultNamespace.Runner
{
    public class MobileInput : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        
        public Vector2 GetInput()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 viewportPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                viewportPosition.x -= 0.5f;
                viewportPosition.x *= 2f;
                viewportPosition.y *= 2f;
                viewportPosition.y = Mathf.Clamp01(viewportPosition.y);
                return viewportPosition;
            }
            return Vector2.zero;
        }
        
        
    }
}