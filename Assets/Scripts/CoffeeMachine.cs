using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class CoffeeMachine : MonoBehaviour, Actionable
{
    


    public KeyCode[] _action_buttons = { movement._left_arm, movement._right_arm };

    public Pickable cup;
    void Start()
    {
        
    }
    void Update()
    {

    }

    public void actOn(movement player, KeyCode action_key)
    {

        foreach (KeyCode _action_button in _action_buttons)
        {
            if (Input.GetKeyDown(_action_button))
            {
                player.pickUp(cup, _action_button);
            } 
        }
        }
    public void preActOn(movement player)
    {
        

        // Ui.popUp(KeyCode.A);
    }


    public void postActOn(movement player)
    {
        // Ui.hide(KeyCode.A);
    }

}
