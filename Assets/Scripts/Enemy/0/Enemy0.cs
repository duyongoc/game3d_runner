﻿using System.Collections;
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

    //enemy's target
    public Transform target;
    public NavMeshAgent agent;
    
    //
    private float moveSpeed = 0f;
    private Rigidbody2D m_rigidbody2D;
    private float distanceWarning = 0;

    //enmey state
    public enum EnemyState { Moving, Attraction, None }
    public EnemyState currentState = EnemyState.Moving;
    

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

                //Camera.main.GetComponent<CameraFollow>().ChangeTarget(transform, 100);
                //SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_scenePauseGame);
                
                StartCoroutine("FinishWarningEnemy0");
                isWarning = true;
            }
        }

        
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        //agent.SetDestination(target.position);
    }

    private void EnenmyAttraction()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator FinishWarningEnemy0()
    {
        yield return new WaitForSeconds(2f);

        warningIcon.SetActive(false);
        
        //Camera.main.GetComponent<CameraFollow>().ChangeTarget(target, 10);
        //SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneInGame);
    }

    //Collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag.Contains("Armor"))
        {
            Instantiate(explosionSpecial, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
