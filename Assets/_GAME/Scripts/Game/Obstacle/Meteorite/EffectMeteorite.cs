using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMeteorite : MonoBehaviour
{
    [Header("Crazy boom effect")]
    public GameObject boomEffect;


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
                boomEffect.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "EnemyElastic":
            case "Tornado":
            case "Obstacle":
                boomEffect.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                Destroy(other.gameObject);
                break;

        }
    }
}
