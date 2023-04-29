using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour, Actionable
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
            print("I am coffee Machineeee\n");
        }
    }
    public void preActOn(movement player)
    {
        player._action_button = _action_button;

        // Ui.popUp(KeyCode.A);
    }


    public void postActOn(movement player)
    {
        // Ui.hide(KeyCode.A);
    }

}
