using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    visitors generator;
    private void Start()
    {
        generator = FindObjectOfType<visitors>();
    }
    public void notify_generator(Visitor vis)
    {
        
        generator.left(vis);   
    }
}
