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

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    [Header("Enmey state")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, Holding, None }

    public Material marDissolve;
    private Material marDefault;
    public GameObject render;

    //
    //= private
    private Rigidbody mRigidbody;
    private Animator mAnimator;

    private float moveSpeed = 0f;
    private Transform target;
    private float distanceWarning = 0;



    //
    // properties
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();

        DissolveObstacle();
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
        mAnimator.SetBool("Dead", true);
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
        GetComponent<Collider>().enabled = false;

        ChangeState(EnemyState.None);
        Invoke("DestroyObject", 3);
    }

    public void TakeForce()
    {
        var vecDir = transform.position - target.position;
        mRigidbody.AddForce(vecDir * 10);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    public void DissolveObstacle()
    {
        render.GetComponent<Renderer>().material = marDissolve;
        GetComponent<Collider>().enabled = false;
        StartCoroutine("OnDissolve");
    }

    IEnumerator OnDissolve()
    {
        float timer = 1;
        float process = 1;

        while (timer >= 0)
        {
            yield return new WaitForSeconds(0.01f);

            timer -= 0.01f;
            process -= 0.01f;
            marDissolve.SetFloat("_processDissolve", process);
        };

        marDissolve.SetFloat("_processDissolve", 0);
        render.GetComponent<SkinnedMeshRenderer>().material = marDefault;
        GetComponent<Collider>().enabled = true;
        // gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "EnemyDefault":
                this.TakeDestroy();
                other.GetComponent<IOnDestroy>()?.TakeDestroy();
                break;

            case "EnemySeek":
            case "PlayerAbility":
                this.TakeDestroy();
                break;
        }

    }


    private void CacheDefine()
    {
        MoveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        warningIcon.SetActive(false);
    }

    private void CacheComponent()
    {
        target = MainCharacter.Instance.GetTransform();
        mRigidbody = GetComponent<Rigidbody>();
        mAnimator = GetComponentInChildren<Animator>();
        marDefault = render.GetComponent<SkinnedMeshRenderer>().material;
    }


}
