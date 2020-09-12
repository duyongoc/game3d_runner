using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyElastic : MonoBehaviour
{
    [Header("Load data Enemy Elastic")]
    public ScriptEnemyElastic scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;

    [Header("Particle preparing")]
    public GameObject parPrepare;

    [Header("State of Enemy")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, Preparing, Attack, Stop, None }

    [Header("Target of enemy")]
    public Transform target;

    [Header("Effect attack")]
    public GameObject affectAttack;

    //
    private float moveSpeed = 0f;
    private Rigidbody m_rigidbody;
    private Animator m_animator;
    public GameObject arrow;
    // private float distanceWarning = 0;

    public float distanceAttack = 5f;

    // Elastic Enemy
    public float timeCharge = 2f;
    public float timeAttack = 0.7f;

    private Vector3 attackPosition;
    private bool isAttack = false;
    private bool isPrepare = false;


    private void LoadData()
    {
        //agent.speed = scriptEnemy.moveSpeed;
        moveSpeed = scriptEnemy.moveSpeed;
    }

    #region  UNITY
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
                    EnemyMoving();

                    break;
                case EnemyState.Preparing:
                    EnemyPreparing();

                    break;
                case EnemyState.Attack:
                    EnemyAttacking();
                    
                    break;
                case EnemyState.None:
                    break;
            }
            // Debug.Log(currentState);
        }
    }
    #endregion

    #region Function of state
    private void EnemyMoving()
    {
        m_animator.SetTrigger("Move");
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        if (Vector3.Distance(this.transform.position, target.position) < distanceAttack)
        {
            ChangeState(EnemyState.Preparing);
        }
    }

    private void EnemyPreparing()
    {
        if (!isPrepare)
        {
            arrow.SetActive(true);
            m_animator.SetTrigger("Prepare");
            Invoke("ChangeStateAttack", timeCharge);
            isAttack = false;
            isPrepare = true;
        }

        transform.LookAt(target.position);       
    }

    private void EnemyAttacking()
    {
        if(isAttack)
            return;
        
        arrow.SetActive(false);
        affectAttack.SetActive(true);
        m_animator.SetTrigger("Attack");
        attackPosition = transform.position + (transform.forward * 15f);
        transform.DOMove(attackPosition, timeAttack, false).SetEase(Ease.OutCubic);

        Invoke("ChangeStateMoving", timeAttack);
        isPrepare = false;
        isAttack = true;
    }
    #endregion

    private void ChangeState(EnemyState state = EnemyState.Attack)
    {
        currentState = state;
    }

    private void ChangeStateAttack()
    {
        currentState = EnemyState.Attack;
    }

    private void ChangeStateMoving()
    {
        currentState = EnemyState.Moving;
        affectAttack.SetActive(false);
    }

    private void ChangeStatePreparing()
    {
        currentState = EnemyState.Preparing;

    }

    public void OnCreateEffectPrepare()
    {
        Instantiate(parPrepare, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IOnDestroy>();
            if (temp != null)
                temp.TakeDestroy();
            var temp2 = other.GetComponentInParent<IOnDestroy>();
            if (temp2 != null)
                temp2.TakeDestroy();

            Instantiate(explosion, transform.localPosition, Quaternion.identity);
        }
        else if (other.tag == "Tornado")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Elastic")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
    }
}
