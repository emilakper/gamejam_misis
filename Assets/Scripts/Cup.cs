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

    override public void onPick(movement obj) 
    {
        obj.GetComponent<SpriteRenderer>().color = Color.yellow;
        Instantiate(to_instance);
    }
}
