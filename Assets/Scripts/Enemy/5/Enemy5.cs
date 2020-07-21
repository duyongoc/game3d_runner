using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    [Header("Load data Enemy 5")]
    public ScriptEnemy5 scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;

    [Header("Particle preparing")]
    public GameObject parPrepare;

    //
    private float moveSpeed = 0f;
    private Rigidbody m_rigidbody;
    private Animator m_animator;
    public GameObject arrow;
    // private float distanceWarning = 0;

    //
    public float distanceAttack = 5f;
    Vector3 vecMove;
    bool check =false;

    //enmey state
    public enum EnemyState { Moving, Preparing, Attack, None }
    public EnemyState currentState = EnemyState.Moving;
    
    [Header("Target of enemy")]
    public Transform target;


    private void LoadData()
    {
        //agent.speed = scriptEnemy.moveSpeed;
        moveSpeed = scriptEnemy.moveSpeed;

    }

    private void Start()
    {
        LoadData();

        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
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
                case EnemyState.Preparing:
                {
                    arrow.SetActive(true);
                    transform.LookAt(target.position);

                    m_animator.SetTrigger("Prepare");
                    Invoke("ChangeStateAttack", 2f);
                    break;
                }
                case EnemyState.Attack:
                {
                    if(!check)
                    {
                        m_animator.SetTrigger("Attack");
                        vecMove =  ( target.position - transform.position).normalized;
                        check = true;
                    }

                    Invoke("ChangeStatePreparing", 2f);
                    arrow.SetActive(false);

                    transform.position += vecMove * Time.deltaTime * moveSpeed * 2;
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
        
        if(Vector3.Distance(this.transform.position, target.position) < distanceAttack)
        {
            ChangeState(EnemyState.Preparing);
        }
        //agent.SetDestination(target.position);
    }

    private void ChangeState (EnemyState state = EnemyState.Attack)
    {
        currentState = state;
    }

    private void ChangeStateAttack()
    {
        currentState = EnemyState.Attack;
    }

    public void OnCreateEffectPrepare()
    {
        Instantiate(parPrepare, transform.position, Quaternion.identity);
    }

    private void ChangeStatePreparing()
    {
        currentState = EnemyState.Preparing;
        check = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tornado")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Ene5")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(other.gameObject);
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
