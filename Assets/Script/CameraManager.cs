using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase _TPS;
    [SerializeField] private CinemachineVirtualCameraBase _FPS;
    [SerializeField] private CinemachineVirtualCameraBase _RTS;

    private bool cam1 = true;
    private void Start()
    {
        SwitchToTPS();
    }

    void Update()
    {
        if (InputHandler.Instance.IsSwitchingCamera())
        {
            InputHandler.Instance.SetRTSCameraMode(false);
            if (cam1)                                                   // Switches back and forth from FPS to Third Person.
            {
                InputHandler.Instance.SetFPSCameraMode(true);
                SwitchToFPS();
            }
            else
            {
                InputHandler.Instance.SetFPSCameraMode(false);
                SwitchToTPS();
            }

            cam1 = !cam1;
        }

        if (InputHandler.Instance.IsSwitchingToRTS())
        {
            InputHandler.Instance.SetRTSCameraMode(true);
            SwitchToRTS();
            cam1 = !cam1;                                              // This ensures that camera returns back to previous camera type before switching to RTS.
        }
    }

    public void SwitchToTPS()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _TPS.Priority = 20;
        _FPS.Priority = 10;
        _RTS.Priority = 5;
    }

    public void SwitchToFPS()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _TPS.Priority = 10;
        _FPS.Priority = 20;
        _RTS.Priority = 5;

    }

    public void SwitchToRTS()
    {
        Cursor.lockState = CursorLockMode.None;
        _TPS.Priority = 10;
        _FPS.Priority = 20;
        _RTS.Priority = 30;
    }
}