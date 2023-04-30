using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIMER : MonoBehaviour
{
    public double time_to_wait = 0;

    /*
        time_to_wait - 100%
        piece - x%
        
        time_to_wait * x = 100 * piece
        x = 100 * piece / time_to_wait
    */

    GameObject time_sprite;
    // Start is called before the first frame update
    void Start()
    {
        time_sprite = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 a = time_sprite.transform.localScale;
        print(time_to_wait / (Time.deltaTime * 100));
        a.x -= (float)( time_to_wait / (Time.deltaTime * 100));
        time_sprite.transform.localScale = a;

    }
}
