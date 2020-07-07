using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy0"))
        {
            var obj = other.gameObject.GetComponentInParent<Enemy0>();
            obj.target = transform;
            obj.currentState = Enemy0.EnemyState.Attraction;
        }
        else if(other.gameObject.CompareTag("Enemy1"))
        {
            var obj = other.gameObject.GetComponentInParent<Enemy1>();
            obj.target = transform;
            obj.currentState = Enemy1.EnemyState.Attraction;
        }
        else if(other.gameObject.CompareTag("Enemy2"))
        {
            var obj = other.gameObject.GetComponent<Enemy2>();
            obj.target = transform;
            obj.currentState = Enemy2.EnemyState.Attraction;
        }

    }
}
