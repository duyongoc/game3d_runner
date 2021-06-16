using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{

    //
    //= inspector
    [Header("CONFIG")]
    [SerializeField] private ScriptTornado scriptTornado;
    [SerializeField] private GameObject explosion;
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Moving, None }


    //
    //= private
    private Rigidbody2D mRigidbody2D;
    private Transform target;
    private float moveSpeed = 0f;


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    private void Update()
    {
        if (!GameMgr.Instance.IsGameRunning)
            return;

        switch (currentState)
        {
            case EnemyState.Moving:
                EnemyMoving();
                break;

            case EnemyState.None:
                break;

        }
    }
    #endregion


    private void EnemyMoving()
    {
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        //agent.SetDestination(target.position);
    }


    private void SelfDestroy()
    {
        explosion.SpawnToGarbage(transform.localPosition, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                explosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                explosion.SpawnToGarbage(transform.localPosition, Quaternion.Euler(-90, 0, 0));
                break;

            case "EnemyElastic":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                SelfDestroy();
                break;

            case "Tornado":
            case "PlayerAbility":
                SelfDestroy();
                break;

            case "Obstacle":
                other.gameObject.GetComponent<StaticObstacle>().DissolveObstacle();
                explosion.SpawnToGarbage(transform.localPosition, Quaternion.Euler(-90, 0, 0));
                break;
        }
        
    }


    private void CacheDefine()
    {
        moveSpeed = scriptTornado.moveSpeed;
        //agent.speed = scriptEnemy.moveSpeed;
    }

    private void CacheComponent()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        target = MainCharacter.Instance.GetTransform();
    }

}

