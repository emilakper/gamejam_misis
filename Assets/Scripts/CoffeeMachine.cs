using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class CoffeeMachine : MonoBehaviour, Actionable
{
 
    public Pickable cup;
    void Start()
    {
        
    }
    void Update()
    {

    }

    public void actOn(movement player, KeyCode action_key)
    {

       
            if (Input.GetKeyDown(action_key) && (!player.left_arm.is_empty() || !player.right_arm.is_empty()))
            {
                player.add_espresso();
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
