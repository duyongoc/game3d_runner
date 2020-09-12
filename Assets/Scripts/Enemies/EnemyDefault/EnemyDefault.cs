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

    [Header("Enemy's target")]
    public Transform target;

    // private variable
    private float moveSpeed = 0f;
    private Rigidbody2D m_rigidbody2D;
    private float distanceWarning = 0;

    #region UNITY
    private void LoadData()
    {
        //agent.speed = scriptEnemy.moveSpeed;
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
    }

    private void Start()
    {
        LoadData();

        warningIcon.SetActive(false);
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = TransformTheBall.GetInstance().GetTransform();

        // StartCoroutine("ParticleMoving", timeParMoving);
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
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

    }

    private void EnenmyHolding()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    #endregion

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
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            if(other.tag == "EnemySeek")
            {
                Instantiate(explosion, transform.localPosition, Quaternion.identity);
                Destroy(this.gameObject);
                return;
            }

            var temp = other.GetComponent<IOnDestroy>();
            if (temp != null)
                temp.TakeDestroy();

            TakeDestroy();
            Destroy(other.gameObject);
        }
        else if (other.tag == "BallPower")
        {
            Instantiate(explosionSpecial, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
