using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShape1 : MonoBehaviour
{
    private bool dirUp = true;
    public float speed = 2.0f;

    public float high = 3.5f;
    private Vector3 pos1;
    private Vector3 pos2;

    private Enemy1 ene;

    void Start()
    {
        ene = GetComponentInParent<Enemy1>();

        pos1 = transform.position;
        pos2 = new Vector3(transform.position.x, high, transform.position.z);
    }

    void Update()
    {
        switch(ene.currentState)
        {
            case Enemy1.EnemyState.Moving:
            {   
                JumpObject();

                break;
            }
            case Enemy1.EnemyState.Attraction:
            {
                transform.localPosition = new Vector3(0f, 0f, 0f);

                break;
            }
            
        }
    }

    private void JumpObject()
    {
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
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp(high, 0 , Mathf.PingPong(Time.time*speed, 1.0f)),
                transform.position.z
            );
        }

        if (transform.position.y >= high)
        {
            dirUp = false;
        }

        if (transform.position.y <= 0)
        {
            dirUp = true;
        }
        
        transform.LookAt(ene.target.position);
    }

}
