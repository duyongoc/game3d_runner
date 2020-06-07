using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public NavMeshAgent agent;
    Rigidbody2D m_rigidbody2D;

    [Header("Move speed")]
    public float moveSpeed = 5f;

    [Header("Enemy dead explosion")]
    public GameObject enemyExplosion;
    
    //enemy's target
    public Transform target;

    //enmey state
    public enum EnemyState{ Moving, Attraction, None }
    public EnemyState currentState = EnemyState.Moving;


    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = TransformTheBall.GetInstance().GetTransform();
    }
    
    private void Update()
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
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        //agent.SetDestination(target.position);
    }

    private void EnenmyAttraction()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        
        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Enemy"))
        {
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
