using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [Header("Load data Tornado")]
    public ScriptTornado scriptTornado;

    [Header("Enemy dead explosion")]
    public GameObject explosion;

    [Header("State of Enemy")]
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, None }
    
    [Header("Enemy's target")]
    public Transform target;

    //
    private float moveSpeed = 0f;
    private Rigidbody2D m_rigidbody2D;

    
    
    private void LoadData()
    {
        //agent.speed = scriptEnemy.moveSpeed;
        moveSpeed = scriptTornado.moveSpeed;

    }

    #region UNITY
    private void Start()
    {
        LoadData();

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
                case EnemyState.None:
                {

                    break;
                }
            }
        }
    }
    #endregion

    #region State of enemy
    private void EnemyMoving()
    {
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        //agent.SetDestination(target.position);
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if( other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IOnDestroy>();
            if(temp != null)
                temp.TakeDestroy();

            Instantiate(explosion, transform.localPosition, Quaternion.Euler(-90,0,0));
        }
        else if (other.tag == "Tornado" || other.tag == "PlayerAbility")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.Euler(-90,0,0));
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstacle")
        {
            other.gameObject.GetComponent<StaticObstacle>().DissolveObstacle();
            Instantiate(explosion, transform.localPosition, Quaternion.Euler(-90,0,0));
        }
        else if(other.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.localPosition, Quaternion.Euler(-90,0,0));

            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }

    }

}
