using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;




public class movement : MonoBehaviour
{

    public KeyCode _left_leg = KeyCode.F;
    public KeyCode _right_leg = KeyCode.G;
    public KeyCode _left_arm = KeyCode.J;
    public KeyCode _right_arm = KeyCode.K;
    public KeyCode _breath = KeyCode.O;
    public KeyCode _blink = KeyCode.E;
    public KeyCode _regain_poisture = KeyCode.Space;


    private bool standing = true;

    private readonly double breath_genkai = 5;

    private bool is_breathing = true;


    private class Arm
    {
        public Object held_object;
    }


    enum Limb  : uint { Right = 0x0, Left = 4294967295 }

    class Actions
    {
        public Limb leg = Limb.Right;
        public Limb arm = Limb.Right;
        public double time_since_blink = 0;
        public double time_since_breath = 0;
    }

    private Actions action = new();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Returns time since last blink
    private double blink()
    {
        double temp = action.time_since_blink;
        action.time_since_blink = 0;
        return temp;
    }

    private double breath() 
    {
        double temp = action.time_since_breath;
        action.time_since_blink = 0;
        return temp;
    }

    private void stopBreathing(bool should_cache = false)
    {
        if (!should_cache)
        {
            action.time_since_breath = 0;
        }
        is_breathing = false;
    }

    private void startBreathing()
    {
        is_breathing = true;
    }


    private void regainPoisture()
    {
        standing = true;
    }
    private void moveLeg(Vector3 direction, Limb prev_limb, Limb curr_limb)
    {
        if (prev_limb == curr_limb)
        {
            return;
        }

        if (!standing)
        {
            return;
        }

        if (prev_limb == Limb.Right)
        {
            transform.position += direction;
            action.leg = Limb.Left;
        }
        else
        {
            transform.position += direction;
            action.leg = Limb.Right;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_right_leg))
        {
           moveLeg(Vector3.right, action.leg, Limb.Right);
        }
        else if (Input.GetKeyDown(_left_leg))
        {
            moveLeg(Vector3.right, action.leg, Limb.Left);
        }

        if (Input.GetKeyDown(_breath))
        {
            breath();
            
        }

        if (Input.GetKeyDown(_blink))
        {
            blink();
        }

        if (Input.GetKeyDown(_regain_poisture))
        {
            if (!standing)
            {
                regainPoisture();
            }
        }

        if (action.time_since_breath > (breath_genkai - Mathf.Epsilon))
        {
            standing = false;
            stopBreathing();
            
        }

        if (is_breathing)
        {
            action.time_since_breath += Time.deltaTime;
        }
            action.time_since_blink += Time.deltaTime;
    }
}
