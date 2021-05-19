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
                var character = other.GetComponent<MainCharacter>();
                character.ChangeState(character.GetCharacterShield);
                SelfDestroy();
            break;
        }
    }


}
