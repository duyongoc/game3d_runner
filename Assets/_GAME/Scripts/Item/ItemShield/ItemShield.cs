using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var temp = other.gameObject.GetComponent<MainCharacter>();
            temp.ChangeState(temp.GetCharacterAbility);

            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
