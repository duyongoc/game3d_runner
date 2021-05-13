using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyDefault")
        {
            var obj = other.gameObject.GetComponent<EnemyDefault>();
            // obj.target = transform;
            obj.currentState = EnemyDefault.EnemyState.Holding;
        }
        else if(other.tag == "EnemyJump")
        {
            var obj = other.gameObject.GetComponentInParent<EnemyJump>();
            // obj.target = transform;
            obj.currentState = EnemyJump.EnemyState.Holding;
        }
        else if(other.tag == "EnemySeek")
        {
            var obj = other.gameObject.GetComponent<EnemySeek>();
            // obj.target = transform;
            obj.currentState = EnemySeek.EnemyState.Holding;
        }

        

    }
}
