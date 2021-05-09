using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyMgr : MonoBehaviour
{
    #region singleton
    public static SpawnEnemyMgr s_instane; 

    private void Awake()
    {
        if(s_instane != null)
        {
            return;
        }
        
        s_instane = this;

        return;
    }

    public static SpawnEnemyMgr GetInstance()
    {
        return s_instane;
    }

    #endregion  
    
    public GameObject[] spawnObject;


    public void SetActiceInPhase(Phase newPhase)
    {   
        foreach(GameObject go in spawnObject)
        {
            go.GetComponent<ISpawnObject>().SetInPhaseObject(false);
            foreach( EnemyInPhase ene in  newPhase.enemies)
            {
                // Debug.Log("go.name" + go.name + " / " + name.enemyType.ToString());
                if(go != null && go.name.Contains(ene.enemyType.ToString()))
                {
                    // Debug.Log("--- / " + name.enemyType.ToString());
                    go.GetComponent<ISpawnObject>().SetInPhaseObject(
                        true, ene.speedIncrease, ene.timeSpawn);
                    
                }
            }
        }
    }

}
