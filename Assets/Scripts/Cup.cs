using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cup : Pickable
{

    public GameObject to_instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public GameObject onPick(movement obj) 
    {
        GameObject instance = Instantiate(to_instance);
        instance.transform.parent = obj.transform;
        instance.transform.localPosition = Vector3.zero;
        instance.GetComponent<SpriteRenderer>().flipX = !Convert.ToBoolean(obj.direction.x + 1);
       
        return instance;
    }
}
