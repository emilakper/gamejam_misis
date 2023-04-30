using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Text;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Visitor : MonoBehaviour
{
    public bool is_leaving = false;
    public bool is_active = false;
    TIMER timer;
    stop occupied = null;

    public void occupy(stop free_chair)
    {
        order_canvas = free_chair.transform.GetChild(2).transform.GetChild(0).GetComponent<Canvas>();
        img_cap = order_canvas.transform.GetChild(0).GetComponent<Image>();
        img_esp = order_canvas.transform.GetChild(1).GetComponent<Image>();
        timer = free_chair.transform.GetChild(3).GetComponent<TIMER>();

        occupied = free_chair;
#if DEBUG
        time_before_ordering = 1;
#else
        time_before_ordering = Random.Range(0, 30);
#endif 
        transform.position = free_chair.seat.transform.position;
    }

    public bool is_occupied()
    {
        return occupied != null;
    }


    public void free_seat()
    {
        img_cap.gameObject.SetActive(false);
        img_esp.gameObject.SetActive(false);
        occupied.onFreed();
        direction = -direction;
        occupied = null;
        is_leaving = true;
    }

    [System.Serializable]
    public class Order
    {
        public float tip_mult = 0.25F;
        private Order(CoffeeType coff)
        {
            coffee = coff;
            max_wait_time = coffee.base_wait_time;
            tip_delta_time = max_wait_time * 0.75;
        }
        static public Order make_new_order()
        {
            return new Order(available_coffees[UnityEngine.Random.Range(0, available_coffees.Length)]);
        }

        //TODO: 
        public int calculacte_reward()
        {
            return coffee.base_reward + (int)(coffee.base_reward * tip_mult) * Convert.ToInt32(wait_time < tip_delta_time);
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
    private Vector3 default_direction;
    private float default_speed;

    Canvas order_canvas;
    Image img_cap;
    Image img_esp;

    private void Start()
    {
        default_direction = direction;
        default_speed = speed;
        
        
    }

    public void res()
    {
        current_order.max_wait_time = 0;
        direction = default_direction;
        speed = default_speed;
        time_before_ordering = 0;
        order_wait = 10;
        is_active = false;
    }

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

                    
                    timer.is_active = true;

                    if (current_order.coffee.name == CoffeeType.Cappuccino.name)
                    {
                        img_cap.gameObject.SetActive(true);
                    }
                    else
                    {
                        img_esp.gameObject.SetActive(true);
                    }
#if DEBUG
                        current_order.max_wait_time = 10;
                        current_order.coffee = CoffeeType.Cappuccino;

#endif

                    timer.time_to_wait = current_order.max_wait_time;
                    timer.gameObject.SetActive(true);
                    timer.res();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag == "ExitDoor")
        {
            if (direction != default_direction)
            {
                print("Hello!");
                ExitDoor a = collision.gameObject.GetComponent<ExitDoor>();
                a.notify_generator(this);

            }

        }
    }
}
