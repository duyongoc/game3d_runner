using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyBoom : MonoBehaviour
{


    [Header("Crazy boom effect")]
    public GameObject boomEffect;
    public int timeDestroy = 5;


    #region UNITY
    private void Start()
    {
        Destroy(this.gameObject, timeDestroy);
    }

    // private void Update()
    // {
    // }
    #endregion


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                boomEffect.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
            case "EnemyElastic":
                boomEffect.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "Elastic":
            case "Tornado":
                boomEffect.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                Destroy(other.gameObject);
                break;
        }
    }
}
