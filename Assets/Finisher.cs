using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finisher : MonoBehaviour, Actionable
{
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
            if (!player.order_cup.is_ready())
            {
                player.order_cup.set_coffee(player.order_cup.pre_cup.finish_coffee());
                print(player.order_cup.coffee.name);

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

