using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInGame : MonoBehaviour
{

    //
    //== inspector 
    [SerializeField] private CONFIG_Phase CONFIG_Phase;

    public Phase[] phases;
    private int indexPhase = 0;
    private bool phaseEnd = false;
    private float timeDt = 0;

    #region UNITY
    private void Start()
    {
        phases = CONFIG_Phase.phases;
        StartPhase();

        GameMgr.Instance.EVENT_RESET_INGAME += Reset;
    }

    private void Update()
    {
        if (phaseEnd || !GameMgr.Instance.IsGameRunning)
            return;


        timeDt += Time.deltaTime;
        if (indexPhase + 1 < phases.Length && timeDt > phases[indexPhase].timePhase)
        {
            SpawnEnemyMgr.Instance.SetActiceInPhase(phases[++indexPhase]);
            timeDt = 0;

            if (indexPhase + 1 == phases.Length)
                phaseEnd = true;
        }

        
    }
    #endregion


    private void StartPhase()
    {
        SpawnEnemyMgr.Instance.SetActiceInPhase(phases[indexPhase]);
    }

    private void CheckZoomCamera()
    {

    }

    public void Reset()
    {
        StartPhase();

        indexPhase = 0;
        phaseEnd = false;
    }



}
