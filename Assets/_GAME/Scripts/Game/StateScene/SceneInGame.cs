using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{

    //
    //=  inspector 
    [Header("All Text")]
    [SerializeField] private GameObject textTapToPlay;
    [SerializeField] private GameObject textSurvival;


    public CameraFollow cameraFollow;
    public bool isPlaying = false;

    [Header("Main Character")]
    public MainCharacter mainCharacter;

    [Header("Sound background in menu game")]
    public AudioClip m_audioBackground;

    [Header("Sound when the ball get power")]
    public AudioClip m_audioPower;

    [Header(" Spawn Obstacle")]
    public SpawnStaticObstacle spawnStaticObstacle;
    public SpawnSoftObstacle spawnSoftObstacle;

    [Header("Make score game")]
    public Text textScore;
    public ScoreMgr scoreMgr;

    
    //
    //= private 
    private GameMgr gameMgr;


    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        if (!gameMgr.IsPlaying && Input.GetMouseButtonDown(0))
        {
            textTapToPlay.SetActive(false);
            textScore.gameObject.SetActive(true);
            textSurvival.SetActive(true);
            textScore.text = scoreMgr.score.ToString("00");
            SoundMgr.GetInstance().PlaySound(m_audioBackground);

            Invoke("SetFalseTextSurvival", 3.5f);

            //setup camera
            cameraFollow.isStart = true;
            
            gameMgr.IsPlaying = true;
            gameMgr.ChangeState(STATEGAME.INGAME);
        }
    }
    #endregion

    public override void StartState()
    {
        base.EndState();
        // Owner.SetActivePanelScene(this.name);

        textTapToPlay.SetActive(true);
        textScore.gameObject.SetActive(false);

        //
        spawnStaticObstacle.isStart = true;
        spawnSoftObstacle.isStart = true;

    }

    public override void UpdateState()
    {
        base.UpdateState();

        //
        if (cameraFollow.IsSetUpCamera())
        {
            isPlaying = true;
        }

        // if (isPlaying)
        // {
        //     scoreMgr.score += Time.deltaTime;
        //     textScore.text = scoreMgr.score.ToString("00");

        //     if (scoreMgr.score > scoreMgr.highscore)
        //         scoreMgr.highscore = (int)scoreMgr.score;

        //     PlayerPrefs.GetInt("highscore", scoreMgr.highscore);
        // }
    }

    public override void EndState()
    {
        base.EndState();

        isPlaying = false;

        // if (!owner.m_sceneGameOver.mainCharacter.isStateMove())
        // scoreMgr.score = 0;
    }


    public void OnPressButtonPauseGame()
    {
        //Owner.ChangeState(Owner.m_pauseGameScene);
    }


    private void SetFalseTextSurvival()
    {
        textSurvival.SetActive(false);
    }


    private void CacheComponent()
    {
        gameMgr = GameMgr.Instance;
    }

}
