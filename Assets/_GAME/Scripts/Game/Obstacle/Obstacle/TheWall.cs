using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWall : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
            case "EnemyElastic":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "Player":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                other.gameObject.SetActive(false);
                break;
        }
    }
}
