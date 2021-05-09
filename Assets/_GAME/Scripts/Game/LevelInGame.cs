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
    }

    private void Update()
    {
        if (phaseEnd)
            return;

        if (GameMgr.Instance.IsStateInGame)
        {
            // Debug.Log("Change phase:--- " );
            timeDt += Time.deltaTime;
            if (indexPhase + 1 < phases.Length && timeDt > phases[indexPhase].timePhase)
            {
                // Debug.Log("Change Phase index: " + indexPhase);
                SpawnEnemyMgr.GetInstance().SetActiceInPhase(phases[++indexPhase]);
                timeDt = 0;

                if (indexPhase + 1 == phases.Length)
                {
                    phaseEnd = true;
                }
            }
        }
    }
    #endregion

    private void StartPhase()
    {
        SpawnEnemyMgr.GetInstance().SetActiceInPhase(phases[indexPhase]);
        //Debug.Log("Change Phase index: " + indexPhase);
    }

    public void Reset()
    {
        indexPhase = 0;
        StartPhase();

        phaseEnd = false;
    }



}
