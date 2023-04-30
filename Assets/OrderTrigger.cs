using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderTrigger : MonoBehaviour, Actionable
{
    stop parent_seat;

    void Start()
    {
        parent_seat = transform.parent.gameObject.GetComponent<stop>();
        gameObject.SetActive(false);
    }
    void Update()
    {

    }

    int check_order(CoffeeType served)
    {
        Visitor visitor = parent_seat._visitor;
        if (visitor.current_order.coffee == served) {
            return visitor.current_order.calculacte_reward();
        }
        return 0;
    }

    public void actOn(movement player, KeyCode action_key)
    {
        
        if (Input.GetKeyDown(action_key))
        {
            player.reward += check_order(player.order_cup.get_coffee());
            print(player.reward);
            gameObject.SetActive(false);
            parent_seat._visitor.free_seat();
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

