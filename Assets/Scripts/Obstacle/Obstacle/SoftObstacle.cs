using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftObstacle : MonoBehaviour
{
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Contains("Enemy"))
        {
            Instantiate(particle, this.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if(other.tag == "Player" || other.tag == "PlayerAbility")
        {
            Instantiate(particle, this.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        
    }

}
