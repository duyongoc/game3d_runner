using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy0")
        {
            var obj = other.gameObject.GetComponent<Enemy0>();
            obj.target = transform;
            obj.currentState = Enemy0.EnemyState.Attraction;
        }
        else if(other.tag == "EnemyJump")
        {
            var obj = other.gameObject.GetComponentInParent<EnemyJump>();
            obj.target = transform;
            obj.currentState = EnemyJump.EnemyState.Holding;
        }
        else if(other.tag == "Enemy3")
        {
            var obj = other.gameObject.GetComponent<Enemy3>();
            obj.target = transform;
            obj.currentState = Enemy3.EnemyState.Attraction;
        }

    }
}
