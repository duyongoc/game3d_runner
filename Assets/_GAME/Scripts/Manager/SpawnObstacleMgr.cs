using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacleMgr : MonoBehaviour
{

    [Header("[Setting]")]
    [SerializeField] private Obstacle[] spawnObject;


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
        foreach (Obstacle item in spawnObject)
        {
            item.Reset();
        }
    }

}
