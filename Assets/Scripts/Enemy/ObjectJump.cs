using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJump : MonoBehaviour
{
    private bool dirUp = true;
    public float speed = 2.0f;

    public float high = 3.5f;
    private Vector3 pos1;
    private Vector3 pos2;

    void Start()
    {
        pos1 = transform.position;
        pos2 = new Vector3(transform.position.x, high, transform.position.z);
    }

    void Update()
    {
        // if (dirUp)
        //     transform.Translate(Vector2.up * speed * Time.deltaTime);
        // else
        //     transform.Translate(-Vector2.up * speed * Time.deltaTime);

        // if (transform.position.y >= 3.5f)
        // {
        //     dirUp = false;
        // }

        // if (transform.position.y <= 0)
        // {
        //     dirUp = true;
        // }

        if(dirUp)
        {

            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp(0, high , Mathf.PingPong(Time.time*speed, 1.0f)),
                transform.position.z
            );
        }
        else
        {
             Debug.Log("b");
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp(high, 0 , Mathf.PingPong(Time.time*speed, 1.0f)),
                transform.position.z
            );
        }

        if (transform.position.y >= 3.5f)
        {
            dirUp = false;
        }

        if (transform.position.y <= 0)
        {
            dirUp = true;
        }
        
        transform.Rotate(Vector3.right * 30f * Time.deltaTime);
    }
}
