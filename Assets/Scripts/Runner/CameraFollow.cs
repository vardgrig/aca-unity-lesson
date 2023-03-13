using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float smoothTime = 1f;


    private Vector3 _offset;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        _offset = targetTransform.position - cameraTransform.position;
    }

    private void LateUpdate()
    {
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetTransform.position - _offset,
            ref velocity, smoothTime);
    }
}