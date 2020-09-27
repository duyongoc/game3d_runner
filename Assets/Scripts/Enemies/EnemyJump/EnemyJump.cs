using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyJump : MonoBehaviour, IOnDestroy
{

    [Header("Load data Enemy Jump")]
    public ScriptEnemyJump scriptEnemy;

    [Header("Animator")]
    public Animator animator;

    [Header("Enemy dead explosion")]
    public GameObject explosion;
    public GameObject jumpExplosion;
    public GameObject shape;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    public float distanceAttack = 5f;

    //
    private float moveSpeed = 0f;
    private Rigidbody2D m_rigidbody2D;
    private float distanceWarning = 0;

    //enmey state
    public enum EnemyState { Stop, Moving, Jumping, Holding, None }
    public EnemyState currentState = EnemyState.Jumping;

    [Header("Enemy's target")]
    public Transform target;

    private float timeWaiting = 2f;
    private float processWaiting = 0f;

    private Vector3 currentTarget;
    private GameObject alertShape = null;

    private void LoadData()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;

    }

    #region UNITY
    private void Start()
    {
        LoadData();

        warningIcon.SetActive(false);
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = TransformTheBall.GetInstance().GetTransform();

        // transform.DOJump( target.position, 5, 3, 3, false );

        currentTarget = target.position;
        SetAlertPlacement(Vector3.zero, false);

    }

    private void FixedUpdate()
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
                case EnemyState.Jumping:
                    {
                        EnemyJumping();
                        break;
                    }
                case EnemyState.Holding:
                    {
                        EnenmyHolding();
                        break;
                    }
                case EnemyState.Stop:
                    {
                        EnemyStop();
                        break;
                    }
                case EnemyState.None:
                    {
                        break;
                    }
            }
            // Debug.Log(currentState);
        }
    }
    #endregion

    #region State FUNCTION
    private void EnemyMoving()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
            animator.SetBool("Jump", false);

        if (isWarning)
        {
            GetWarningFromEnemy();
        }

        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(this.transform.position, target.position) < distanceAttack)
        {
            Vector3 vec = (target.position - transform.position).normalized;
            currentTarget = target.position + vec * 3f;
            SetAlertPlacement(new Vector3(currentTarget.x, 0.5f, currentTarget.z), true);

            
            ChangeState(EnemyState.Jumping);
        }
    }

    private void EnemyStop()
    {
        //animator.SetBool("Moving", false);

        transform.LookAt(target.position);
        processWaiting += Time.deltaTime;

        if (processWaiting > timeWaiting)
        {
            ChangeState(EnemyState.Moving);
            SetAlertPlacement(Vector3.zero, false);

            processWaiting = 0;
        }
    }

    private void EnemyJumping()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            animator.SetBool("Jump", true);

        if (gameObject != null)
            transform.DOJump(currentTarget, 5f, 1, 1.2f, false);

        ChangeState(EnemyState.Stop);
    }

    private void EnenmyHolding()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private void SetAlertPlacement(Vector3 pos, bool active)
    {
        if (alertShape == null)
        {
            alertShape = Instantiate(shape);
        }
        alertShape.SetActive(active);
        alertShape.transform.position = pos;
    }

    private void GetWarningFromEnemy()
    {
        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
        {
            warningIcon.SetActive(true);
            StartCoroutine("FinishWarningEnemyJump");
        }
    }

    public void SetWarning(bool warning)
    {
        isWarning = warning;
    }

    IEnumerator FinishWarningEnemyJump()
    {
        yield return new WaitForSeconds(2f);
        isWarning = false;
        warningIcon.SetActive(false);
    }

    //Collision
    public void TakeDestroy()
    {
        animator.SetBool("Dead", true);
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
        GetComponent<Collider>().enabled = false;
        Destroy(alertShape.gameObject);
        
        ChangeState(EnemyState.None);
        Invoke("DestroyObject", 3);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyJump" || other.tag == "EnemySeek")
        {
            var temp = other.GetComponent<IOnDestroy>();
            if (temp != null)
                temp.TakeDestroy();

            TakeDestroy();
        }
        else if (other.tag == "AlertShape")
        {
            Instantiate(jumpExplosion, transform.localPosition, Quaternion.identity);
        }
        else if(other.tag == "PlayerAbility")
        {
            Instantiate(jumpExplosion, transform.localPosition, Quaternion.identity);
            this.TakeDestroy();
        }

    }
}
