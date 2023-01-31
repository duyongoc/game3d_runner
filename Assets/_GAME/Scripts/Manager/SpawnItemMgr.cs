using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemMgr : MonoBehaviour
{

    [Header("[Setting]")]
    [SerializeField] private Item[] spawnObject;


    #region UNITY
    private void Start()
    {
        GameMgr.Instance.EVENT_RESET_INGAME += Reset;
    }

    // private void Update()
    // {
    // }
    #endregion
    

    public void Reset()
    {
        foreach (Item item in spawnObject)
        {
            item.Reset();
        }
    }


}
