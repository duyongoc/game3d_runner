using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy0 : MonoBehaviour
{
    [Header("Load data Enemy 0")]
    public ScriptEnemy0 scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    // [Header("Particle when enemy moving")]
    // public GameObject parMoving;
    // public float timeParMoving = 0f;
    // public bool isParMoving = true;

    // private variable
    private float moveSpeed = 0f;
    private Rigidbody2D m_rigidbody2D;
    private float distanceWarning = 0;

    [Header("Enmey state")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, Attraction, None }
    
    //enemy's target
    public Transform target;

    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }

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
                {
                    EnemyMoving();
                    break;
                }
                case EnemyState.Attraction:
                {
                    EnenmyAttraction();
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
        if(!isWarning)
        {
            if(Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
            {
                warningIcon.SetActive(true);
                SceneMgr.GetInstance().GetComponentInChildren<SpawnEnemy0>().FinishWarningAlert();

                StartCoroutine("FinishWarningEnemy0");
                isWarning = true;
            }
        }

        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

    private void EnenmyAttraction()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator FinishWarningEnemy0()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
    }

    //Collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            
            var temp = other.GetComponentInParent<Enemy1>();
            if(temp)
                Destroy(temp.gameObject);
            Destroy(other.gameObject);
            
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag.Contains("Armor"))
        {
            Instantiate(explosionSpecial, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }

    // IEnumerator ParticleMoving(float time)
    // {
    //     while(isParMoving)
    //     {
    //         yield return new WaitForSeconds(time);
    //         Instantiate(parMoving, transform.position, Quaternion.identity);
    //     }
    // }

}
