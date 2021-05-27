using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{

    //
    //=  inspector 
    [Header("Param")]
    [SerializeField] private GameObject virtualMovement;

    [Header("Text display")]
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private GameObject textTapToPlay;
    [SerializeField] private GameObject textSurvival;


    //
    //= private 
    private GameMgr gameMgr;
    private ScoreMgr scoreMgr;
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
        UpdateScore();
        CheckStartGame();
    }
    #endregion


    private void UpdateScore()
    {
        if (!gameMgr.IsGameRunning)
            return;

        scoreMgr.score += Time.deltaTime;
        float minutes = Mathf.FloorToInt(scoreMgr.score / 60);
        float seconds = Mathf.FloorToInt(scoreMgr.score % 60);
        txtScore.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (scoreMgr.score > scoreMgr.highscore)
            scoreMgr.highscore = (int)scoreMgr.score;
        PlayerPrefs.GetFloat("HighScore", scoreMgr.highscore);
    }

    private void CheckStartGame()
    {
        if (!gameMgr.IsPlaying && Input.GetMouseButtonDown(0))
        {
            SetUpStartGame();
            cameraFollow.HasStart = true;

            gameMgr.IsPlaying = true;
            gameMgr.ChangeState(STATEGAME.INGAME);
        }
    }

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
    }

    private void CacheComponent()
    {
        gameMgr = GameMgr.Instance;
        scoreMgr = ScoreMgr.Instance;
        cameraFollow = CameraFollow.Instance;
        character = MainCharacter.Instance;
    }


}
