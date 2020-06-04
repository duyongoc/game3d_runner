using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public NavMeshAgent agent;
    Rigidbody2D m_rigidbody2D;

    public float speed = 5f;
    
    public Transform target;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        

    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target.position, Time.deltaTime * speed);
        
        //agent.SetDestination(target.position);
    }
}
