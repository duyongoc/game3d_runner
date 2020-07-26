using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public int score;
    public GameObject prefabsTextCoin;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "BallPower")
        {
            ScoreMgr.GetInstance().PlusScore(score);
            Instantiate(prefabsTextCoin, transform.position, Quaternion.identity);
            
            Destroy(this.gameObject);
        }
    }
}
