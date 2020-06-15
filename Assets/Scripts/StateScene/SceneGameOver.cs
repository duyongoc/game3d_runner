using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneGameOver : StateScene
{
    [Header("Position original")]
    [SerializeField] private int m_originX = default;
    [SerializeField] private int m_originY = default;

    [Tooltip("Speed time duration when change scene")]
    [SerializeField] private float m_speedDuration = 0.5f;

    [Header("Show score")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Sound when player dead")]
    public AudioClip m_audioEnd;

    [Header("Reset game")]
    public TheBall theBall;
    public SpawnEnemy1 spawnEnemy1;
    public SpawnPower spawnPower;
    public SpawnTheHole spawnTheHole;

    [Header("Make score game")]
    public ScoreMgr scoreMgr;

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);

        scoreText.text = "Score: " +  scoreMgr.score;
        highScoreText.text = "HighScore: " + scoreMgr.highscore;
        this.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, m_speedDuration);

        //sound
        SoundMgr.GetInstance().StopSound();
        SoundMgr.GetInstance().PlaySoundOneShot(m_audioEnd);
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

        this.GetComponent<RectTransform>().DOAnchorPos(new Vector2(m_originX, m_originY), m_speedDuration);
    }

    #region Events of button
    public void OnPressButtonReplay()
    {
        Reset();
        Owner.ChangeState(Owner.m_sceneInGame);
        
    }

    public void OnPressButtonMenu()
    {
        Reset();
        owner.ChangeState(Owner.m_sceneMenu);
    }

    public void OnPressButtonExit()
    {
        Application.Quit();

    }
    #endregion

    private void Reset()
    {   
        spawnEnemy1.Reset();
        spawnPower.Reset();
        spawnTheHole.Reset();
        scoreMgr.Reset();
        
        theBall.Reset();
    }
}
