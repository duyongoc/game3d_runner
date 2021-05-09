using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum STATEGAME
{
    MENU,
    TUTORIAL,
    INGAME,
    GAMEOVER,
    NONE
}

public class GameMgr : Singleton<GameMgr>
{


    //
    //= public 
    public CONFIG_GAME CONFIG_GAME;


    //
    //= private 
    private bool isSkipTutorial;
    private bool isMovingCamera;


    //
    //= private
    private STATEGAME currentState = STATEGAME.NONE;


    //
    //= properties
    public bool IsStateInGame { get => currentState == STATEGAME.INGAME; }


    private void LoadData()
    {
        isSkipTutorial = CONFIG_GAME.isSkipTutotial;
        isMovingCamera = CONFIG_GAME.isMovingCamera;
    }


    #region UNTIY
    // private void Start()
    // { 
    // }

    // private void Update()
    // {
    // }
    #endregion





}
