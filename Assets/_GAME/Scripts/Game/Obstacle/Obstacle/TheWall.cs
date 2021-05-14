using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IDamage>();
            temp?.TakeDamage(0);
        }
        else if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        
    }
}
