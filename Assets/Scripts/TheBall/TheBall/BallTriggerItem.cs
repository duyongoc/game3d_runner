using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerItem : MonoBehaviour
{
    [Header("Gun Item")]
    public GameObject bullet;
    public int count = 3;

    IEnumerator TriggerItemGun()
    {
        while(count > 0)
        {
            float randX = Random.Range(-0.5f, 0.5f);
            float speed = Random.Range(400, 500);

            GameObject tmp =  Instantiate(bullet, transform.position, transform.rotation);
            tmp.GetComponent<cube>().vectorDir = new Vector2(randX, 1);
            tmp.GetComponent<cube>().speed = speed;

            count--;
            yield return new WaitForSeconds(0.6f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GunItem")
        {
            count = 5;
            StartCoroutine("TriggerItemGun");
        }
    }


}
