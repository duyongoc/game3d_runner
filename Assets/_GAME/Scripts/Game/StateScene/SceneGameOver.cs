using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneGameOver : StateScene
{

    //
    //== inspector
    [Header("Buttons in GameOver Scene")]
    [SerializeField] private UIButton btRePlay;
    [SerializeField] private UIButton btMenu;
    [SerializeField] private UIButton btnQuit;

    [Header("Show text score")]
    [SerializeField] private TMP_Text scoreText;


    //
    //= private 
    private GameMgr gameMgr;
    private ScoreMgr scoreMgr;



    #region UNITY
    private void Start()
    {
        CacheComponent();

        btRePlay.OnClick.OnTrigger.Event.AddListener(OnClickButtonReplay);
        btMenu.OnClick.OnTrigger.Event.AddListener(OnClickButtonMenu);
        btnQuit.OnClick.OnTrigger.Event.AddListener(OnClickButtonExit);

        float minutes = Mathf.FloorToInt(scoreMgr.score / 60);
        float seconds = Mathf.FloorToInt(scoreMgr.score % 60);
        scoreText.text = "TIME: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // private void Update()
    // {
    // }
    #endregion


    public void OnClickButtonReplay()
    {
        gameMgr.LoadReplayGame();
    }

    public void OnClickButtonMenu()
    {
        gameMgr.LoadMenuGame();
    }

    public void OnClickButtonExit()
    {
        Application.Quit();
    }


    private void CacheComponent()
    {
        gameMgr = GameMgr.Instance;
        scoreMgr = ScoreMgr.Instance;
    }
}
