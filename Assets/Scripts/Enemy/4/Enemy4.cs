using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [Header("Load data Enemy 4")]
    public ScriptEnemy4 scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;

    //enemy's target
    public Transform target;
    
    //
    private float moveSpeed = 0f;
    private Rigidbody2D m_rigidbody2D;
    // private float distanceWarning = 0;

    //enmey state
    public enum EnemyState { Moving, None }
    public EnemyState currentState = EnemyState.Moving;
    
    private void LoadData()
    {
        //agent.speed = scriptEnemy.moveSpeed;
        moveSpeed = scriptEnemy.moveSpeed;

    }

    private void Start()
    {
        LoadData();

        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = TransformTheBall.GetInstance().GetTransform();
    }

    private void Update()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            switch (currentState)
            {
                case EnemyState.Moving:
                {
                    EnemyMoving();
                    break;
                }
                case EnemyState.None:
                {

                    break;
                }
            }
        }
    }

    private void EnemyMoving()
    {
        
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        //agent.SetDestination(target.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tornado")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstacle")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            other.gameObject.SetActive(false);
            
        }
        else if(other.gameObject.tag == "TheBall")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag.Contains("Enemy"))
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            
            var temp = other.GetComponentInParent<Enemy1>();
            if(temp)
                Destroy(temp.gameObject);
            Destroy(other.gameObject);
        }

    }

}
