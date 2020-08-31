using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : MonoBehaviour
{
    [Header("Load data Enemy Seek")]
    public ScriptEnemySeek scriptEnemy;

    [Header("Enemy dead Explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;

    [Header("Warning the Player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    // private variable
    private Rigidbody2D m_rigidbody2D;
    private float moveSpeed = 0f;
    private float slowdownTurning = 0f;
    private float distanceWarning = 0f;
    private Vector3 veclocity = Vector3.zero;

    [Header("Enmey state")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, Attractive, None }

    //enemy's target
    public Transform target;

    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }

    private void LoadData()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        slowdownTurning = scriptEnemy.slowdownTurning;
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
                    {
                        EnemyMoving();
                        break;
                    }
                case EnemyState.Attractive:
                    {
                        EnenmyAttractive();
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
        if (!isWarning)
        {
            if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
            {
                warningIcon.SetActive(true);
                SceneMgr.GetInstance().GetComponentInChildren<SpawnEnemySeek>().FinishWarningAlert();

                StartCoroutine("FinishWarningEnemySeek");
                isWarning = true;
            }
        }

        Vector3 vec = new Vector3(target.position.x, 0, target.position.z);
        transform.LookAt(vec);

        Vector3 distance = (target.position - transform.position);
        Vector3 desired = distance.normalized * moveSpeed;
        Vector3 steering = desired - veclocity;

        veclocity += steering * Time.deltaTime;
        transform.position += veclocity * Time.deltaTime;
        
    }

    private void EnenmyAttractive()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator FinishWarningEnemySeek()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
    }

    //Collision
    public void TakeDestroy()
    {
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IOnDestroy>();
            if(temp != null)
                temp.TakeDestroy();

            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.tag == "BallPower")
        {
            Instantiate(explosionSpecial, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
