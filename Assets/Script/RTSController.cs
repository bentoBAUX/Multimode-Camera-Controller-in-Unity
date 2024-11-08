using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RTSController : MonoBehaviour
{
    private CinemachineInputProvider _inputProvider;
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _cameraTransform;

    private CinemachineComponentBase _framingTransposer;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _panSpeed = 2f;
    [SerializeField] private float _zoomSpeed = 3f;
    [SerializeField] private float _zoomInMax = 40f;
    [SerializeField] private float _zoomOutMax = 90f;
    [SerializeField] private float _transitionSmoothness = 3f;

    private void Awake()
    {
        _inputProvider = GetComponent<CinemachineInputProvider>();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraTransform = _virtualCamera.VirtualCameraGameObject.transform;
    }

    private void Start()
    {
        _framingTransposer = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    private void Update()
    {
        if (!InputHandler.IsRTSCameraMode)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_playerTransform.position.x, this.transform.position.y,
                _playerTransform.position.z), Time.deltaTime * _transitionSmoothness);
        }

        // From here on by samyam: https://youtu.be/PsAbHoB85hM
        float x = InputHandler.Instance.GetPanInput().x;
        float y = InputHandler.Instance.GetPanInput().y;
        float z = InputHandler.Instance.GetZoomInput();

        if (x != 0 || y != 0)
        {
            PanScreen(x, y);
        }

        if (z != 0)
        {
            ZoomScreen(z);
        }
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
            _cameraTransform.position + new Vector3(direction.x, 0, direction.y), Time.deltaTime * _panSpeed);
    }

    public void ZoomScreen(float z)
    {
        float fov = _virtualCamera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + z, _zoomInMax, _zoomOutMax);
        _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, target, _zoomSpeed * Time.deltaTime);
    }
}
