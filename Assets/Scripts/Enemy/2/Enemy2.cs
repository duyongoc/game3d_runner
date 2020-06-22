using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [Header("Load data Enemy 2")]
    public ScriptEnemy2 scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    //enemy's target
    public Transform target;
    private Rigidbody2D m_rigidbody2D;
    private float distanceWarning;
    private float moveSpeed;
    
    //enmey state
    public enum EnemyState{ Moving, Attraction, None }
    public EnemyState currentState = EnemyState.Moving;

    private void LoadData()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
    }

    private void Start()
    {
        LoadData();
        
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = TransformTheBall.GetInstance().GetTransform();
    }
    
    void Update()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            switch(currentState)
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
                SceneMgr.GetInstance().GetComponentInChildren<SpawnEnemy2>().FinishWarningAlert();


                // Camera.main.GetComponent<CameraFollow>().ChangeTarget(transform, 100);
                // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_scenePauseGame);
                
                StartCoroutine("FinishWarningEnemy2");
                isWarning = true;
            }
        }


        transform.position = Vector3.MoveTowards(transform.position,target.position, Time.deltaTime * moveSpeed);
        //transform.LookAt(target.position);
        //agent.SetDestination(target.position);
    }

    private void EnenmyAttraction()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);     
        //transform.LookAt(target.position);

        if(Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator FinishWarningEnemy2()
    {
        yield return new WaitForSeconds(2f);

        warningIcon.SetActive(false);
        // Camera.main.GetComponent<CameraFollow>().ChangeTarget(target, 10);
        // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneInGame);
    }

    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Enemy"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag.Contains("Armor"))
        {
            Instantiate(explosionSpecial, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
    
}
