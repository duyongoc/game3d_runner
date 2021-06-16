using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftObstacle : MonoBehaviour
{

    //
    //= ínpector
    [SerializeField] private GameObject particle;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
            case "EnemyElastic":
                particle.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                gameObject.SetActive(false);
                break;

            case "Player":
            case "PlayerAbility":
                particle.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }

    }

}
