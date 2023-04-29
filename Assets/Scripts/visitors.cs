using UnityEngine;

public class visitors : MonoBehaviour
{
    public float speed = 50f;
    void FixedUpdate()
    {
        Vector3 right = Vector3.right;
        float timeSinceLastFrame = Time.deltaTime;

        Vector3 translation = right*timeSinceLastFrame * speed; 

        transform.Translate(translation);

    }

}
