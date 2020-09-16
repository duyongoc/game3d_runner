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

    [Header("Camera Follow")]
    public CameraFollow cameraFollow;

    [Header("Reset the ball")]
    public TheBall theBall;

    [Header("Reset Obstacle")]
    public ObstacleController obstacleController;

    [Header("Spawn gameobject")]
    public SpawnEnemyDefault spawnEnemyDefault;
    public SpawnEnemyGlobe spawnEnemyGlobe;
    public SpawnEnemyJump spawnEnemyJump;
    public SpawnEnemySeek spawnEnemySeek;

    public SpawnTornado spawnTornado;
    public SpawnElastic spawnElastic;
    public SpawnCrazyPlace crazyPlace;

    [Header("Spawn other Enemy")]
    public SpawnTheHole spawnTheHole;
    public SpawnMeteorite spawnMeteorite;
    
    [Header("Spawn other object")]
    public SpawnItemCoin spawnItemCoin;
    public SpawnItemSpeed spawnItemSpeed;
    public SpawnItemShield spawnItemShield;
    public SpawnItemFire spawnItemFire;
    

    [Header(" Ball change color reset")]
    public BallChangeColor ballChangeColor;

    [Header("Make score game")]
    public ScoreMgr scoreMgr;

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);

        scoreText.text = "Score: " +  scoreMgr.score.ToString("00");
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
        // reset score Mgr
        scoreMgr.Reset();

        //camera
        cameraFollow.Reset();

        //obstacle
        obstacleController.Reset();

        // spawn enemy
        spawnEnemyDefault.Reset();
        spawnEnemyGlobe.Reset();
        spawnEnemyJump.Reset();
        spawnEnemySeek.Reset();
        spawnTornado.Reset();
        spawnElastic.Reset();
        crazyPlace.Reset();
        
        //item
        spawnItemCoin.Reset();
        spawnItemSpeed.Reset();
        spawnItemShield.Reset();
        spawnItemFire.Reset();

        //other object
        spawnMeteorite.Reset();
        spawnTheHole.Reset();

        //the ball
        ballChangeColor.Reset();
        theBall.Reset();
    }
}
