using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShape1 : MonoBehaviour
{
    [Header("Load data Enemy 1")]
    public ScriptEnemy1 scriptEnemy1;

    private bool directionUp = true;
    private float jumpSpeed = 0f;
    private float jumpHigh = 0f;

    //
    private Vector3 pos1;
    private Vector3 pos2;

    private Enemy1 ene;

    private void LoadData()
    {
        jumpSpeed = scriptEnemy1.jumpSpeed;
        jumpHigh = scriptEnemy1.jumpHigh;
    }

    private void Start()
    {
        LoadData();

        ene = GetComponentInParent<Enemy1>();
        pos1 = transform.position;
        pos2 = new Vector3(transform.position.x, jumpHigh, transform.position.z);
    }

    private void Update()
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
        if(directionUp)
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp( 0, jumpHigh , Mathf.PingPong(Time.time * jumpSpeed, 1.0f)),
                transform.position.z
            );
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp( jumpHigh, 0 , Mathf.PingPong(Time.time * jumpSpeed, 1.0f)),
                transform.position.z
            );
        }

        if (transform.position.y >= jumpHigh)
        {
            directionUp = false;
        }

        if (transform.position.y <= 0)
        {
            directionUp = true;
        }
        
        transform.LookAt(ene.target.position);
    }

}
