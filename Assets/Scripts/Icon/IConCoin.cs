using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IConCoin : MonoBehaviour
{
    public int score;
    public GameObject prefabsTextCoin;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TheBall"
            || other.gameObject.tag == "Armor")
        {
            ScoreMgr.GetInstance().PlusScore(score);
            Instantiate(prefabsTextCoin, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
