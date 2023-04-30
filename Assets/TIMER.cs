using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIMER : MonoBehaviour
{
    public bool is_active = false; 
    public double time_to_wait = 0;
    Vector3 base_vec;

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
        base_vec = time_sprite.transform.localScale;
        gameObject.SetActive(false);
    }

    public void res()
    {
        time_sprite = transform.GetChild(1).gameObject;
        time_sprite.transform.localScale = base_vec;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!is_active) return;
        Vector3 a = time_sprite.transform.localScale;
        if (a.x > 0) 
            a.x -= base_vec.x * (float)(Time.deltaTime/ time_to_wait);
        time_sprite.transform.localScale = a;

    }
}
