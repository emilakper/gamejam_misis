using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkShelf : MonoBehaviour, Actionable
{
    public void preActOn(movement mov) { }
    public void actOn(movement mov, KeyCode key) 
    { 
        if (Input.GetKeyDown(key))
        {
            mov.add_milk();
        }
    }
   
    public void postActOn(movement mov)
    {

    }
}
