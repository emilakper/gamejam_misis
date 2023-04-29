using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Visitor : MonoBehaviour
{

    public bool is_active = false;
    
    stop occupied = null;

    public void occupy(stop free_chair)
    {
        occupied = free_chair;
        time_before_ordering = Random.Range(0, 30);
        transform.position = free_chair.seat.transform.position;
    }

    public bool is_occupied()
    {
        return occupied != null;
    }


    public void free_seat()
    {
        occupied.onFreed();
        occupied = null;
    }

    [System.Serializable]
    public class Order
    {
        private Order(CoffeeType coff)
        {
            coffee = coff;
            max_wait_time = coffee.base_wait_time;
            tip_delta_time = max_wait_time * 0.75;
        }
        static public Order make_new_order()
        {
            return new Order(available_coffees[Random.Range(0, available_coffees.Length)]);
        }

        //TODO: 
        public int calculacte_reward()
        {
            return coffee.base_reward;
        }


        public double get_time()
        {
            return wait_time;
        }

        public void increase_time()
        {
            wait_time += Time.deltaTime;
        }
        
        public CoffeeType coffee;
        private double wait_time = 0;
        public double max_wait_time = 0;
        public double tip_delta_time;

        static CoffeeType[] available_coffees = { CoffeeType.Espresso, CoffeeType.Cappuccino };
    }

    public Order current_order;


    public float speed = 50f;

    public double time_before_ordering = 0;
    public double order_wait = 10;

    private double counter_before_ordering;


    public Vector3 direction = Vector3.forward;
    void FixedUpdate()
    {
        if (!is_active) return;

        if (!is_occupied())
        {
            float timeSinceLastFrame = Time.deltaTime;

            Vector3 translation = direction * timeSinceLastFrame * speed;

            transform.Translate(translation);
        }
        else
        {
            

            if (counter_before_ordering > time_before_ordering)
            {

                if (current_order.max_wait_time == 0)
                {
                    current_order = Order.make_new_order();
                }
                else if (current_order != null)
                {
                    current_order.increase_time();
                    if (current_order.get_time() > current_order.max_wait_time)
                    {
                        free_seat();

                    }
                }
            }
            else
            {
                counter_before_ordering += Time.deltaTime;
            }
                 

        }

    }
}
