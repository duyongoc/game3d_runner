using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMgr : MonoBehaviour
{
    [Header("Config the game")]
    public ConfigGame scriptConfigGame;

    public static CheatMgr s_instane; 

    public bool isSkipTutorial;
    public bool isMovingCamera;

    private void LoadData()
    {
        isSkipTutorial = scriptConfigGame.isSkipTutotial;
        isMovingCamera = scriptConfigGame.isMovingCamera;
    }

    private void Awake()
    {
        if(s_instane != null)
        {
            return;
        }
        s_instane = this;
        LoadData();

        return;
    }

    public static CheatMgr GetInstance()
    {
        return s_instane;
    }

    


}
