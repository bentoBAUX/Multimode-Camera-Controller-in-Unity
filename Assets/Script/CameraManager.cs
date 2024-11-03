using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase _vCam1;
    [SerializeField] private CinemachineVirtualCameraBase _vCam2;
    [SerializeField] private CinemachineVirtualCameraBase _vCam3;

    private bool cam1 = true;
    private void Start()
    {
        SwitchToVCam1();
    }

    void Update()
    {
        if (InputHandler.Instance.IsSwitchingCamera())
        {
            InputHandler.Instance.SetRTSCameraMode(false);
            if (cam1)
            {
                SwitchToVCam2();
            }
            else
            {
                SwitchToVCam1();
            }

            cam1 = !cam1;
        }

        if (InputHandler.Instance.IsSwitchingToRTS())
        {
            InputHandler.Instance.SetRTSCameraMode(true);
            SwitchToVCam3();
        }
    }

    public void SwitchToVCam1()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _vCam1.Priority = 20;
        _vCam2.Priority = 10;
        _vCam3.Priority = 5;
    }

    public void SwitchToVCam2()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _vCam1.Priority = 10;
        _vCam2.Priority = 20;
        _vCam3.Priority = 5;

    }

    public void SwitchToVCam3()
    {
        Cursor.lockState = CursorLockMode.None;
        _vCam1.Priority = 10;
        _vCam2.Priority = 20;
        _vCam3.Priority = 30;
    }
}