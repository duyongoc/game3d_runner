using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInGame : StateScene
{
    [Header("Tap to play in scene in game")]
    public GameObject textTapToPlay;
    public bool isPlaying = false;

    [Header("Sound background in menu game")]
    public AudioClip m_audioBackground;

    [Header("Sound when the ball get power")]
    public AudioClip m_audioPower;
    
    [Header("Create first enemy")]
    public GameObject enemyFirst;

    [Header("Make score game")]
    public Text textScore;
    public ScoreMgr scoreMgr;
    private float countTime = 0;

    [Header("Slider process")]
    public GameObject sliderProcess;

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);
        
        textTapToPlay.SetActive(true);
        textScore.gameObject.SetActive(false);

        if(!sliderProcess.activeSelf)
        {
            sliderProcess.SetActive(true);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if(!isPlaying)
        {
            if(Input.GetMouseButtonDown(0))
            {
                textTapToPlay.SetActive(false);
                textScore.gameObject.SetActive(true);
                SoundMgr.GetInstance().PlaySound(m_audioBackground);

                isPlaying = true;
            }
        }

        if(isPlaying)
        {
            countTime += Time.deltaTime;
            scoreMgr.score = (int)countTime;
            textScore.text = scoreMgr.score.ToString("00.##");


            if (scoreMgr.score > scoreMgr.highscore)
                scoreMgr.highscore = scoreMgr.score;

            PlayerPrefs.GetInt("highscore", scoreMgr.highscore);
        }
    }

    public override void EndState()
    {
        base.EndState();

        isPlaying = false;

        if(!owner.m_sceneGameOver.theBall.isStateBallMove())
            countTime = 0;
    }

    #region Handler event of button
    public void OnPressButtonPauseGame()
    {
        //Owner.ChangeState(Owner.m_pauseGameScene);
    }
    #endregion
    
}
