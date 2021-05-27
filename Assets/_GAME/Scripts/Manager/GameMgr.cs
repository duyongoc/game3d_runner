using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
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
    //= event 
    public Action EVENT_RESET_INGAME;


    //
    //= public 
    public CONFIG_GAME CONFIG_GAME;


    //
    //= private 
    private bool isPlaying = false;


    //
    //= private
    private STATEGAME currentState = STATEGAME.NONE;
    private float timePlay = 0f;


    //
    //= properties
    public bool IsGameRunning { get => currentState == STATEGAME.INGAME; }
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
    public float GetTimePlay { get => timePlay; }



    #region UNTIY
    // private void Start()
    // { 
    // }

    // private void Update()
    // {
    // }

    private void FixedUpdate()
    {
        timePlay += Time.deltaTime;

    }
    #endregion


    public void ChangeState(STATEGAME newState)
    {
        currentState = newState;
    }



    public void LoadReplayGame()
    {
        Reset();
        ChangeState(STATEGAME.INGAME);
    }

    public void LoadMenuGame()
    {
        ChangeState(STATEGAME.MENU);
        GameEventMessage.SendEvent("LoadMenu");
    }

    public void LoadGameOver()
    {
        ChangeState(STATEGAME.GAMEOVER);
        StartCoroutine(Utils.DelayEvent(() => { GameEventMessage.SendEvent("LoadGameOver"); }, 2.5f));
    }


    public void Reset()
    {
        timePlay = 0f;
        SoundMgr.PlaySound(SoundMgr.Instance.SFX_BACKGROUND);
        EVENT_RESET_INGAME?.Invoke();
    }

}


// God bless my code to be bug free 
//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//