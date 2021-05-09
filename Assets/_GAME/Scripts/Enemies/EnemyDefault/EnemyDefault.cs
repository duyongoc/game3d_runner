using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour, IOnDestroy
{
    [Header("Load data Enemy 0")]
    public ScriptEnemyDefault scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;

    [Header("Animation")]
    public Animator animator;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    [Header("Enmey state")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, Holding, None }

    [Header("Enemy's target")]
    public Transform target;

    // private variable
    private float moveSpeed = 0f;
    private Rigidbody m_rigidbody;
    private float distanceWarning = 0;

    //
    // property
    //
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    #region UNITY
    private void LoadData()
    {
        //agent.speed = scriptEnemy.moveSpeed;
        MoveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
    }

    private void Start()
    {
        LoadData();

        warningIcon.SetActive(false);
        m_rigidbody = GetComponent<Rigidbody>();
        target = MainCharacter.Instance.GetTransform();

        // StartCoroutine("ParticleMoving", timeParMoving);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void Update()
    {
        if (GameMgr.Instance.IsStateInGame)
        {
            switch (currentState)
            {
                case EnemyState.Moving:
                    EnemyMoving();

                    break;
                case EnemyState.Holding:
                    EnenmyHolding();

                    break;
                case EnemyState.None:
                    break;
            }
        }
    }
    #endregion

    #region Function of State
    private void EnemyMoving()
    {
        if (isWarning)
        {
            GetWarningFromEnemy();
        }

        Vector3 vec = new Vector3(target.position.x, 0, target.position.z);
        transform.LookAt(vec);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * MoveSpeed);
    }

    private void EnenmyHolding()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * MoveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    #endregion

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private void GetWarningFromEnemy()
    {
        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
        {
            warningIcon.SetActive(true);
            StartCoroutine("FinishWarningEnemyDefault");
        }
    }

    public void SetWarning(bool warning)
    {
        isWarning = warning;
    }

    IEnumerator FinishWarningEnemyDefault()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
        isWarning = false;
    }

    //Collision
    public void TakeDestroy()
    {
        animator.SetBool("Dead", true);
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
        GetComponent<Collider>().enabled = false;
        
        ChangeState(EnemyState.None);
        Invoke("DestroyObject", 3);
    }

    public void TakeForce()
    {
        var vecDir = transform.position - target.position;
        m_rigidbody.AddForce(vecDir * 10);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyDefault")
        {
            var temp = other.GetComponent<IOnDestroy>();
            if (temp != null)
                temp.TakeDestroy();
            this.TakeDestroy();
        }
        else if(other.tag == "EnemySeek")
        {
            this.TakeDestroy();
        }
        else if(other.tag == "PlayerAbility")
        {
            this.TakeDestroy();
            // this.TakeForce();
        }
        
    }



}
