using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(particle, other.transform.position, Quaternion.identity);
        if (other.tag == "EnemyDefault" || other.tag == "EnemySeek")
        {
            var temp = other.GetComponent<IOnDestroy>();
            if(temp != null)
                temp.TakeDestroy();
        }
        else if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "PlayerAbility")
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
