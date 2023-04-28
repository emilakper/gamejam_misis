using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;




public class movement : MonoBehaviour
{

    public KeyCode left_leg = KeyCode.F;
    public KeyCode right_leg = KeyCode.G;
    public KeyCode left_arm = KeyCode.J;
    public KeyCode right_arm = KeyCode.K;
    public KeyCode breath = KeyCode.O;
    public KeyCode blink = KeyCode.E;


    private class Arm
    {
        public Object held_object;
    }


    enum Limb { None = 0x0, Right = 0x1, Left = 0x2 }

    class Actions
    {
        public Limb leg = Limb.None;
        public Limb arm = Limb.None;
        public double time_since_blink = 0;
        public double time_since_breath = 0;
    }

    private Actions action = new();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(right_leg) && (action.leg == Limb.Right || action.leg == Limb.None))
        {
            transform.position += Vector3.right;
            action.leg = Limb.Left;
        }
        if (Input.GetKeyDown(left_leg) && (action.leg == Limb.Left || action.leg == Limb.None))
        {
            transform.position += Vector3.right;
            action.leg = Limb.Right;
        }


        action.time_since_breath += Time.deltaTime;
        action.time_since_blink += Time.deltaTime;
    }
}
