using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public NavMeshAgent agent;
    Rigidbody2D m_rigidbody2D;

    public float speed = 5f;
    
    public Transform target;


    public enum EnemyState{ Moving, Attraction, None }
    public EnemyState currentState = EnemyState.Moving;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();

    }
    
    void Update()
    {
        switch(currentState)
        {
            case EnemyState.Moving:
            {
                EnemyMoving();
                break;
            }
            case EnemyState.Attraction:
            {
                EnenmyAttraction();
                break;
            }
            case EnemyState.None:
            {

                break;
            }

        }
    }

    private void EnemyMoving()
    {
        //transform.position = Vector3.MoveTowards(transform.position,target.position, Time.deltaTime * speed);
        //transform.LookAt(target.position);
        agent.SetDestination(target.position);
    }

    private void EnenmyAttraction()
    {
        agent.SetDestination(target.position);
           
        //transform.LookAt(target.position);

        
    }
    
}
