using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class stop : MonoBehaviour
{

    public GameObject seat;
    public GameObject order_trigger;

    public Visitor _visitor; 

    private void Start()
    {
        seat = transform.GetChild(0).gameObject;
        order_trigger = transform.GetChild(1).gameObject;
    }


    void grab_visitor(Visitor visitor)
    {
        if (!visitor.is_leaving)
        {
            _visitor = visitor;
            visitor.occupy(this);
            // TODO: activate later maybe?
            order_trigger.gameObject.SetActive(true);

        }
    }
        public void onFreed()
    {
        isEmpty = true;
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
