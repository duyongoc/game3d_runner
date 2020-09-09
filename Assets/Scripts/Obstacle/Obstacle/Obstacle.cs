using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(particle, other.transform.position, Quaternion.identity);
        if (other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IOnDestroy>();
            if(temp != null)
                temp.TakeDestroy();
                
            var temp2 = other.GetComponentInParent<IOnDestroy>();
            if(temp2 != null)
                temp2.TakeDestroy();
        }
        else if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "")
        {
            var temp = other.GetComponent<IOnDestroy>();
            temp.TakeDestroy();
        }
        
    }
}
