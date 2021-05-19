using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "EnemyDefault":
                other.GetComponent<EnemyDefault>().EffectFromTheHole(this.transform);
                break;

            case "EnemyJump":
                other.GetComponent<EnemyJump>().EffectFromTheHole(this.transform);
                break;

            case "EnemySeek":
                other.GetComponent<EnemySeek>().EffectFromTheHole(this.transform);
                break;
        }

    }
}
