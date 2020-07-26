using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyBoom : MonoBehaviour
{
    [Header("Crazy boom effect")]
    public GameObject boomEffect;

    public int timeDestroy = 5;

    private void Start()
    {
        Destroy(this.gameObject, timeDestroy);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IOnDestroy>();
            if(temp != null)
                temp.TakeDestroy();
            var temp2 = other.GetComponentInParent<IOnDestroy>();
            if(temp2 != null)
                temp2.TakeDestroy();

            Instantiate(boomEffect, transform.localPosition, Quaternion.identity);
        }
        else if(other.tag == "Player")
        {
            Instantiate(boomEffect, other.transform.position, Quaternion.identity);

            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "Elastic" || other.tag == "Tornado")
        {
            Instantiate(boomEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject); 
        }     
    }
}
