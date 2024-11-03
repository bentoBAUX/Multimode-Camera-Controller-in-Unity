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

    private bool cam1 = true;

    private void Start()
    {
        SwitchToVCam1();
    }

    void Update()
    {
        if (InputHandler.Instance.IsSwitchingCamera())
        {
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
    }

    public void SwitchToVCam1()
    {
        _vCam1.Priority = 20;
        _vCam2.Priority = 10;
    }

    public void SwitchToVCam2()
    {
        _vCam1.Priority = 10;
        _vCam2.Priority = 20;
    }
}