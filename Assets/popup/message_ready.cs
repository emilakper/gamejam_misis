using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class message_ready : MonoBehaviour
{
    public GameObject Message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Message.SetActive(true);
            Invoke("DeactivateMessage", 3f);
        }
    }

    private void DeactivateMessage()
    {
        Message.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Message.SetActive(false);
        }
    }
}
