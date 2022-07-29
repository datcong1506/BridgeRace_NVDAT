using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool Inverst=true;
    private Camera _camera;

    private void Start()
    {
        _camera=Camera.main;
    }

    private void LateUpdate()
    {
        LookCam();
    }
    private void LookCam()
    {
        if (Inverst)
        {
            transform.LookAt(transform.position-_camera.transform.position+transform.position);
        }
        else
        {
            transform.LookAt(_camera.transform.position);
        }
    }
}
