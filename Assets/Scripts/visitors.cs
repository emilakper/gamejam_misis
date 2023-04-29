using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class visitors : MonoBehaviour
{
    public int number_of_visitors = 3;

  
    public GameObject visitor;

    private List<GameObject> visitors_left = new List<GameObject>();
    private void Start()
    {

        SpriteRenderer visitor_sprite = visitor.GetComponent<SpriteRenderer>();
        print(visitor_sprite.sprite.textureRect.width);
        Vector3 points = transform.position;
        for (int i = 0; i < number_of_visitors; i++)
        {
            visitors_left.Add(Instantiate(visitor, points, transform.rotation));

        }
    }

    private void Update()
    {
        
    }
}
