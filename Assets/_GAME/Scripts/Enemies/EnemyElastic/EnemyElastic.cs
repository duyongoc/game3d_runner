using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyElastic : MonoBehaviour, IOnDestroy
{
    [Header("Load data Enemy Elastic")]
    public ScriptEnemyElastic scriptEnemy;

    [Header("Animation")]
    public Animator animator;

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
        target = MainCharacter.Instance.GetTransform();
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
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
            //Debug.Log(currentState);
        }
    }
    #endregion

    #region Function of state
    private void EnemyMoving()
    {
        animator.SetTrigger("Move");
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
            animator.SetTrigger("Attack");
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
        // animator.SetTrigger("Attack");
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

    //Collision
    public void TakeDestroy()
    {
        CancelInvoke();
        arrow.SetActive(false);
        DOTween.Kill(transform); 
        ChangeState(EnemyState.None);

        animator.SetBool("Dead", true);
        Instantiate(explosion, transform.localPosition, Quaternion.identity);

        GetComponent<Collider>().enabled = false;
        Invoke("DestroyObject", 3);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);

            var temp = other.GetComponent<IOnDestroy>();
            if (temp != null)
                temp.TakeDestroy();
        }
        else if (other.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            other.gameObject.SetActive(false);
            // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "Obstacle")
        {
            other.gameObject.GetComponent<StaticObstacle>().DissolveObstacle();
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
        }
        else if(other.tag == "PlayerAbility")
        {
            this.TakeDestroy();
        }
    }
}
