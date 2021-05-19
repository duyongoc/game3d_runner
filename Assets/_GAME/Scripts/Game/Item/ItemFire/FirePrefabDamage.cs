using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePrefabDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IDamage>();
            temp?.TakeDamage(0);

            var temp2 = other.GetComponentInParent<IDamage>();
            temp2?.TakeDamage(0);
        }


    }
}
