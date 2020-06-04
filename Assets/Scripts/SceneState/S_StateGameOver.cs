using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_StateGameOver : S_StateBase
{

    [Header("Show score")]
    public Text scoreText;
    public Text highScoreText;

    public override void StartState()
    {
        base.EndState();
        owner.SetActivePanelScene(this.name);

        // scoreText.text = "Score: " +  Score.score;
        // highScoreText.text = "HighScore: " + Score.highscore;
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

    }

    #region Events of button
    public void OnPressButtonReplay()
    {
        Reset();
        owner.ChangeState(owner.m_inGameScene);
    }

    public void OnPressButtonExit()
    {
        Application.Quit();
    }
    #endregion

    private void Reset()
    {
        
        //Score.score = 0;
    }


}
