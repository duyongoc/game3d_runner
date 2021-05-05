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
            Instantiate(particle, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if(other.tag == "Player" || other.tag == "PlayerAbility")
        {
            Instantiate(particle, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            gameObject.SetActive(false);
        }
        
    }

}
