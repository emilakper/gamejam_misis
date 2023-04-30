using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public Camera currentCamera;
    public Camera nextCamera;

    void Start()
    {
        currentCamera.enabled = true;
        nextCamera.enabled = false;
    }

    public void SwitchCamera()
    {
        currentCamera.enabled = false;
        nextCamera.enabled = true;

        Camera temp = currentCamera;
        currentCamera = nextCamera;
        nextCamera = temp;
    }

}
