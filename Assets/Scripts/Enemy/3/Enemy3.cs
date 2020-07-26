using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour, IOnDestroy
{
    [Header("Load data Enemy 3")]
    public ScriptEnemy3 scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;
    public int heath = 100;
    
    [Header("Particle armor")]
    public GameObject particleArmor;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    //enemy's target
    public Transform target;
    private Rigidbody2D m_rigidbody2D;
    private float moveSpeed = 0;
    private float distanceWarning;

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
                    //EnenmyAttraction();
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
                SceneMgr.GetInstance().GetComponentInChildren<SpawnEnemy3>().FinishWarningAlert();
                
                //
                Camera.main.GetComponent<CameraFollow>().isFlowCamera = true;
                StartCoroutine("FinishWarningEnemy3");

                isWarning = true;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position,target.position, Time.deltaTime * moveSpeed);
    }

    IEnumerator FinishWarningEnemy3()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
    }

    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }

    //Collider
    public void TakeDestroy()
    {
        heath -= 50;
        if(heath <= 0)
        {
            Instantiate(explosionSpecial, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
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
        else if(other.gameObject.tag.Contains("BallPower"))
        {
            Instantiate(explosionSpecial, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
