using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerItem : MonoBehaviour
{
    [Header("Item Fire")]
    public GameObject fireOfItem;
    public GameObject itemFireEffect;
    public int count = 5;

    Vector3[] vector = {
        new Vector3(0.1f, 1f, 0f),
    };

    IEnumerator TriggerItemFire()
    {
        while(count > 0)
        {
            //float randX = Random.Range(-0.1f, 0.1f);
            GameObject tmp =  Instantiate(fireOfItem, transform.position, transform.rotation);
            tmp.GetComponent<FirePrefab>().vectorDir = new Vector2(0, 1);

            count--;
            yield return new WaitForSeconds(0.6f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ItemFire")
        {
            count = 5;
            StartCoroutine("TriggerItemFire");
            
            Instantiate(itemFireEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        
    }
}
