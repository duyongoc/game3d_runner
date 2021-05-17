using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyMgr : Singleton<SpawnEnemyMgr>
{

    //
    //= inspector
    [SerializeField] private SpawnEnemy[] spawnObject;


    #region UNITY
    private void Start()
    {
        GameMgr.Instance.EVENT_RESET_INGAME += Reset;
    }

    // private void Update()
    // {
    // }
    #endregion


    public void SetActiceInPhase(Phase newPhase)
    {
        foreach (SpawnEnemy item in spawnObject)
        {
            item.GetComponent<ISpawnObject>().SetInPhaseObject(false);
            foreach (EnemyInPhase ene in newPhase.enemies)
            {
                // Debug.Log("go.name" + go.name + " / " + name.enemyType.ToString());
                if (item != null && item.name.Contains(ene.enemyType.ToString()))
                {
                    // Debug.Log("--- / " + name.enemyType.ToString());
                    item.GetComponent<ISpawnObject>().SetInPhaseObject(true, ene.speedIncrease, ene.timeSpawn);
                }
            }
        }
    }


    public void Reset()
    {
        foreach (SpawnEnemy item in spawnObject)
        {
            item.Reset();
        }
    }
}
