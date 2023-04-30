using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class visitors : MonoBehaviour
{
    [SerializeField]
    public int number_of_visitors = -1;
    public double right_border_of_waiting = 15;

    private double time_before_next_visitor = 0;

    private int currently_spawned_visitors_c = 0;

    public GameObject visitor;


    private double counter_since_last_visitor = 0;


    private List<GameObject> visitors_left = new List<GameObject>();
    private void Start()
    {
        if (number_of_visitors == -1)
        {
            number_of_visitors = FindObjectsOfType<stop>().Length;
        }

        SpriteRenderer visitor_sprite = visitor.GetComponent<SpriteRenderer>();
        Vector3 points = transform.position;
        for (int i = 0; i < number_of_visitors; i++)
        {
            visitors_left.Add(Instantiate(visitor, points, transform.rotation));
            visitors_left[i].GetComponent<Visitor>().is_active = false;
        }
    }


    public void left(Visitor vis)
    {
        vis.is_leaving = false;
        int indx = visitors_left.FindIndex(x => x.name == vis.name);
        GameObject tmp = visitors_left[visitors_left.Count - 1]; ;
        visitors_left[visitors_left.Count - (1 + (number_of_visitors - currently_spawned_visitors_c))] = visitors_left[indx];
        visitors_left[indx] = tmp;
        visitors_left[visitors_left.Count - 1].gameObject.GetComponent<Visitor>().res();
        currently_spawned_visitors_c -= 1;
    }
    /*
     *  Visitor begins his movement
     *  Time before next visitor is updated
     *  When Visitor returns, he is pushed to the begging of the List
     */
    private void Update()
    {
        if (currently_spawned_visitors_c < number_of_visitors)
        {
            if (counter_since_last_visitor > time_before_next_visitor)
            {
                visitors_left[currently_spawned_visitors_c].GetComponent<Visitor>().is_active = true;
                time_before_next_visitor = Random.Range(5, (float)right_border_of_waiting);
                currently_spawned_visitors_c += 1;
                counter_since_last_visitor = 0;
            }
            counter_since_last_visitor += Time.deltaTime;
        }


            //visitors_left.Add(visitors_left[0]);
            //visitors_left.Remove(visitors_left[0]);
        }
    }
