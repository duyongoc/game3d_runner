using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.UI;
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



    [Header("Show score")]
    public Text scoreText;
    public Text highScoreText;

    public ScoreMgr scoreMgr;

    [Header("Sound when player dead")]
    public AudioClip m_audioEnd;

    [Header("Camera Follow")]
    public CameraFollow cameraFollow;

    [Header("Reset the ball")]
    public MainCharacter mainCharacter;

    [Header("Reset Obstacle")]
    public SpawnStaticObstacle spawnStaticObstacle;
    public SpawnSoftObstacle spawnSoftObstacle;

    [Header("Spawn gameobject")]
    public SpawnEnemyDefault spawnEnemyDefault;
    public SpawnEnemyElastic spawnEnemyElastic;
    public SpawnEnemyGlobe spawnEnemyGlobe;
    public SpawnEnemyJump spawnEnemyJump;
    public SpawnEnemySeek spawnEnemySeek;

    public SpawnTornado spawnTornado;
    public SpawnCrazyPlace crazyPlace;

    [Header("Spawn other Enemy")]
    public SpawnTheHole spawnTheHole;
    public SpawnMeteorite spawnMeteorite;

    [Header("Spawn other object")]
    public SpawnItemCoin spawnItemCoin;
    public SpawnItemSpeed spawnItemSpeed;
    public SpawnItemShield spawnItemShield;
    public SpawnItemFire spawnItemFire;


    //
    //= private 
    private GameMgr gameMgr;



    #region UNITY
    private void Start()
    {
        CacheComponent();

        btRePlay.OnClick.OnTrigger.Event.AddListener(OnClickButtonReplay);
        btMenu.OnClick.OnTrigger.Event.AddListener(OnClickButtonMenu);
        btnQuit.OnClick.OnTrigger.Event.AddListener(OnClickButtonExit);
    }

    // private void Update()
    // {
    // }
    #endregion


    public override void StartState()
    {
        base.EndState();

        //sound
        SoundMgr.Instance.StopSound();
        SoundMgr.Instance.PlaySoundOneShot(m_audioEnd);
    }


    public void OnClickButtonReplay()
    {
        // Reset();
        gameMgr.LoadReplayGame();
    }

    public void OnClickButtonMenu()
    {
        // Reset();
        gameMgr.LoadMenuGame();
    }

    public void OnClickButtonExit()
    {
        Application.Quit();

    }


    private void Reset()
    {
        scoreMgr.Reset();
        cameraFollow.Reset();

        spawnStaticObstacle.Reset();
        spawnSoftObstacle.Reset();

        spawnEnemyDefault.Reset();
        spawnEnemyElastic.Reset();
        spawnEnemyGlobe.Reset();
        spawnEnemyJump.Reset();
        spawnEnemySeek.Reset();
        spawnTornado.Reset();
        crazyPlace.Reset();

        spawnItemCoin.Reset();
        spawnItemSpeed.Reset();
        spawnItemShield.Reset();
        spawnItemFire.Reset();

        spawnMeteorite.Reset();
        spawnTheHole.Reset();
    }


    private void CacheComponent()
    {
        gameMgr = GameMgr.Instance;
    }
}
