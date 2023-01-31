using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{

    [Header("[Setting]")]
    public GameObject prefabsTextCoin;
    public int score;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
            case "PlayerAbility":
                prefabsTextCoin.SpawnToGarbage(transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                break;

        }
    }


}
