using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{

    //
    //= inspector 
    [SerializeField] private GameObject particle;
    


    private void SelfDestroy()
    {
        particle.SpawnToGarbage(transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                PowerController.Instance.shieldPower.TriggerAbility(0);
                SelfDestroy();
            break;
        }
    }


}
