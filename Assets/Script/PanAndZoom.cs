using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PanAndZoom : MonoBehaviour
{
    private CinemachineInputProvider _inputProvider;
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _cameraTransform;

    [SerializeField] private float _panSpeed = 2f;

    private void Awake()
    {
        _inputProvider = GetComponent<CinemachineInputProvider>();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraTransform = _virtualCamera.VirtualCameraGameObject.transform;
    }

    private void Update()
    {
        float x;
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;

        if (y >= Screen.height * 0.95f)
        {
            direction.y += 1;
        } else if (y <= Screen.height * 0.05f)
        {
            direction.y -= 1;
        }

        if (x >= Screen.width * 0.95f)
        {
            direction.x += 1;
        } else if (x <= Screen.width * 0.05f)
        {
            direction.x -= 1;
        }

        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position,
            _cameraTransform.position + (Vector3)direction, Time.deltaTime * _panSpeed);
    }
}
