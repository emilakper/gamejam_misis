using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<CameraSwitch>().SwitchCamera();
    }
}
