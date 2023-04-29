using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConcreteActionable : MonoBehaviour, Actionable
{
    public KeyCode _action_button = KeyCode.A;
    void Start()
    {
    }
    void Update()
    {

    }

    public void actOn(movement player, KeyCode action_key)
    {
        if (Input.GetKeyDown(action_key))
        {
            print("aa\n");
        }
        }
        public void preActOn(movement player)
    {
        player._action_button = _action_button;
    }


    public void postActOn(movement player)
    {

    }
  
}
