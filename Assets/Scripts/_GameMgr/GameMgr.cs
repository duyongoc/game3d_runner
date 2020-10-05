using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    #region singleton
    public static GameMgr s_instane; 

    private void Awake()
    {
        if(s_instane != null)
        {
            return;
        }
        
        LoadData();
        s_instane = this;

        return;
    }
    #endregion  

    [Header("Config the game")]
    public ConfigGame configGame;

    [Header("Config the scene")]
    public ConfigScene configScene;

    public Phase[] phases;

    public bool isSkipTutorial;
    public bool isMovingCamera;

    private float timeDt = 0;
    private int indexPhase = 0;
    private bool phaseEnd = false;

    private void LoadData()
    {
        isSkipTutorial = configGame.isSkipTutotial;
        isMovingCamera = configGame.isMovingCamera;

        phases = configScene.phases;
    }

    private void StartPhase()
    {
        SpawnEnemyMgr.GetInstance().SetActiceInPhase(phases[indexPhase]);
        //Debug.Log("Change Phase index: " + indexPhase);
    }

    #region UNTIY
    private void Start()
    {
        StartPhase();   
    }

    private void Update()
    {
        if(phaseEnd)
            return;

        if (SceneMgr.GetInstance().IsStateInGame())
        {
            Debug.Log("Change phase:--- " );
            timeDt += Time.deltaTime;
            if(indexPhase + 1 < phases.Length && timeDt > phases[indexPhase].timePhase)
            {
                Debug.Log("Change Phase index: " + indexPhase);
                SpawnEnemyMgr.GetInstance().SetActiceInPhase(phases[++indexPhase]);
                timeDt = 0;

                if(indexPhase + 1 == phases.Length)
                {
                    phaseEnd = true;
                }
            }
        }

    }
    #endregion

    public void Reset()
    {
        indexPhase = 0;
        StartPhase();
        
        phaseEnd = false;
    }

    public static GameMgr GetInstance()
    {
        return s_instane;
    }
}
