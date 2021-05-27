using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMgr : Singleton<ScoreMgr>
{

    //
    //= public
    public float score;
    public float highscore;


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
        score = 0;
    }



}
