using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{
    [Header("Tap to play in scene in game")]
    public GameObject textTapToPlay;
    public bool isPlaying = false;
    public CameraFollow cameraFollow;

    [Header("Main Character")]
    public MainCharacter mainCharacter;

    [Header("Sound background in menu game")]
    public AudioClip m_audioBackground;

    [Header("Sound when the ball get power")]
    public AudioClip m_audioPower;

    [Header(" Spawn Obstacle")]
    public SpawnStaticObstacle spawnStaticObstacle;
    public SpawnSoftObstacle spawnSoftObstacle;

    [Header("Text Intro")]
    public GameObject textSurvival;
    
    [Header("Make score game")]
    public Text textScore;
    public ScoreMgr scoreMgr;

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);
        
        textTapToPlay.SetActive(true);
        textScore.gameObject.SetActive(false);

        //
        spawnStaticObstacle.isStart = true;
        spawnSoftObstacle.isStart = true;
        isPlaying = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //Debug.Log("isPlaying: " + isPlaying + " textTapToPlay.SetActive " + textTapToPlay.activeSelf);
        
        if(!isPlaying)
        {
            if(Input.GetMouseButtonDown(0))
            {
                textTapToPlay.SetActive(false);
                textScore.gameObject.SetActive(true);
                SoundMgr.GetInstance().PlaySound(m_audioBackground);

                textSurvival.SetActive(true);
                Invoke("SetFalseTextSurvival", 3.5f);
                textScore.text = scoreMgr.score.ToString("00");

                //setup camera
                cameraFollow.isStart = true;
            }
            //
            if(cameraFollow.IsSetUpCamera())
            {
                isPlaying = true;
            }
        }

        if(isPlaying)
        {
            scoreMgr.score += Time.deltaTime;
            textScore.text = scoreMgr.score.ToString("00");

            if (scoreMgr.score > scoreMgr.highscore)
                scoreMgr.highscore = (int)scoreMgr.score;

            PlayerPrefs.GetInt("highscore", scoreMgr.highscore);
        }
    }

    public override void EndState()
    {
        base.EndState();

        isPlaying = false;

        if(!owner.m_sceneGameOver.mainCharacter.isStateMove())
            scoreMgr.score = 0;
    }

    #region Handler event of button
    public void OnPressButtonPauseGame()
    {
        //Owner.ChangeState(Owner.m_pauseGameScene);
    }
    #endregion


    private void SetFalseTextSurvival()
    {
        textSurvival.SetActive(false);
    }
    
}
