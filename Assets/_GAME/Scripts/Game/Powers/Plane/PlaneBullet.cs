using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBullet : MonoBehaviour
{

    // [private] 
    private Vector3 mTarget;
    private float moveSpeed = 0f;
    private GameObject bulletExplosion = null;


    #region UNITY
    // private void Start()
    // {
    // }

    private void Update()
    {
        if (mTarget == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, mTarget, moveSpeed * Time.deltaTime);

        // if (gameObject.activeSelf)
        //     transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    #endregion



    public void Init(Vector3 target, float speed, GameObject explosion)
    {
        mTarget = target;
        moveSpeed = speed;
        bulletExplosion = explosion;
    }


    private void SelfDestroy()
    {
        bulletExplosion?.SpawnToGarbage(transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
            case "EnemyElastic":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                SelfDestroy();
                break;

            case "Tornado":
            case "TheGround":
                SelfDestroy();
                break;
        }
    }

}
