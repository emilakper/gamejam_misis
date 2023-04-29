using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stop : MonoBehaviour
{

    public GameObject sitting;
    bool isEmpty = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEmpty)
        {
            GameObject.Destroy(other.GameObject());
            sitting.SetActive(true);
            isEmpty = false;
        }
    }
}
