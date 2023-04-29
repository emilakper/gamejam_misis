using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color new_color = sprite.color;
        new_color.a = 0;
        sprite.color = new_color;
    }

    public void res()
    {
        Color b = sprite.color;
        b.a = 0;
        sprite.color = b;
    }
    // Update is called once per frame
    void Update()
    {
        movement player = FindObjectOfType<movement>();
        if (player.action.time_since_blink > player.blinking_offset_when_screen_starts_fading)
        {
            Color b = sprite.color;
            b.a = ((float)(1 - (float)(player.blink_genkai - (player.action.time_since_blink - player.blinking_offset_when_screen_starts_fading)) / player.blink_fading_speed));
            sprite.color = b;
        }
    }
}
