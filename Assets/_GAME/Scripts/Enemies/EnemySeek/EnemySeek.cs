using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : MonoBehaviour, IOnDestroy
{
    [Header("Load data Enemy Seek")]
    public ScriptEnemySeek scriptEnemy;

    [Header("Animator")]
    public Animator animator;

    [Header("Enemy dead Explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;

    [Header("Warning the Player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    [Header("Set effect up when enemy turning")]
    public GameObject prefabsParTurning;
    public float timeTurning = 0.2f;
    private float processTurning = 0f;

    // private variable
    private Rigidbody2D m_rigidbody2D;
    private float moveSpeed = 0f;
    private float slowdownTurning = 0f;
    private float distanceWarning = 0f;
    private Vector3 veclocity = Vector3.zero;

    [Header("Enmey state")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, Holding, None }

    [Header("Enemy's target")]
    public Transform target;

    //
    // property
    //
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }

    private void LoadData()
    {
        MoveSpeed = scriptEnemy.moveSpeed;
        slowdownTurning = scriptEnemy.slowdownTurning;
        distanceWarning = scriptEnemy.distanceWarning;
    }

    #region UNITY
    private void Start()
    {
        LoadData();

        warningIcon.SetActive(false);
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = TransformTheBall.GetInstance().GetTransform();
    }

    private void FixedUpdate()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
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

        //seeking the target - MC
        Vector3 distance = (target.position - transform.position);
        Vector3 desired = distance.normalized * MoveSpeed;
        Vector3 steering = desired - veclocity;
        veclocity += steering * Time.deltaTime;
        transform.position += veclocity * Time.deltaTime;

        // trail effect
        float dot = transform.eulerAngles.y - target.eulerAngles.y;
        //Debug.Log(transform.eulerAngles.y + " - " + target.eulerAngles.y + " =  " + dot);

        //check spawn trail temporary, need to research other way better
        bool hasTrail = Mathf.Abs(dot) > 40f && Mathf.Abs(dot) < 100f;
        if (hasTrail)
        {
            processTurning += Time.deltaTime;
            if (processTurning > timeTurning)
            {
                Instantiate(prefabsParTurning, transform.position, Quaternion.identity);
                processTurning = 0;
            }
        }
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
            StartCoroutine("FinishWarningEnemySeek");
        }
    }

    IEnumerator FinishWarningEnemySeek()
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

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "EnemySeek" || other.tag == "EnemyJump")
        {
            var temp = other.GetComponent<IOnDestroy>();
            if (temp != null)
                temp.TakeDestroy();
                
            this.TakeDestroy();
        }
        if (other.tag == "PlayerAbility")
        {
            Instantiate(explosionSpecial, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
