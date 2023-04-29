using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stop : MonoBehaviour
{

    public GameObject seat;

    private Visitor _visitor; 

    private void Start()
    {
        seat = GameObject.Find("Seat");
    }


    void grab_visitor(Visitor visitor)
    {
        _visitor = visitor;
        visitor.occupy(this);
        
    }

    public void onFreed()
    {
        print("freed");
    }

    bool isEmpty = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEmpty && other.gameObject.layer == LayerMask.NameToLayer("Visitors"))
        { 
            grab_visitor(other.GetComponent<Visitor>());
            isEmpty = false;
        }
    }
}
