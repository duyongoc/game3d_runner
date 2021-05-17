using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{

    //
    //=  inspector 
    [Header("Param")]
    [SerializeField] private GameObject virtualMovement;

    [Header("Text display")]
    [SerializeField] private GameObject textTapToPlay;
    [SerializeField] private GameObject textSurvival;


    [Header(" Spawn Obstacle")]
    public SpawnStaticObstacle spawnStaticObstacle;
    public SpawnSoftObstacle spawnSoftObstacle;

    [Header("Make score game")]
    public Text textScore;
    public ScoreMgr scoreMgr;


    //
    //= private 
    private GameMgr gameMgr;
    private CameraFollow cameraFollow;
    private MainCharacter character;


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();

        character.EVENT_PLAYER_DEAD += OnEventPlayerDead;
        GameMgr.Instance.EVENT_RESET_INGAME += OnEventResetGame;
    }

    private void Update()
    {
        if (gameMgr.IsPlaying)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            SetUpStartGame();
            cameraFollow.isStart = true;

            gameMgr.IsPlaying = true;
            gameMgr.ChangeState(STATEGAME.INGAME);
        }
    }
    #endregion


    private void SetUpStartGame()
    {
        textTapToPlay.SetActive(false);
        textSurvival.SetActive(true);
        virtualMovement.SetActive(true);

        SoundMgr.PlaySound(SoundMgr.Instance.SFX_BACKGROUND);
        StartCoroutine(Utils.DelayEvent(() => { textSurvival.SetActive(false); }, 3f));
    }


    private void OnEventPlayerDead()
    {
        virtualMovement.SetActive(false);
    }

    private void OnEventResetGame()
    {
        virtualMovement.SetActive(true);
    }

    public void OnPressButtonPauseGame()
    {

    }

    private void CacheDefine()
    {
        textTapToPlay.SetActive(true);
        virtualMovement.SetActive(false);

        // spawnStaticObstacle.isStart = true;
        // spawnSoftObstacle.isStart = true;
    }

    private void CacheComponent()
    {
        gameMgr = GameMgr.Instance;
        cameraFollow = CameraFollow.Instance;
        character = MainCharacter.Instance;
    }

    // textScore.gameObject.SetActive(true);
    // textScore.text = scoreMgr.score.ToString("00");
    //     scoreMgr.score += Time.deltaTime;
    //     textScore.text = scoreMgr.score.ToString("00");
    //     if (scoreMgr.score > scoreMgr.highscore)
    //         scoreMgr.highscore = (int)scoreMgr.score;
    //     PlayerPrefs.GetInt("highscore", scoreMgr.highscore);

}
