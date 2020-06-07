using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneGameOver : StateScene
{
    
    [Header("Show score")]
    public Text scoreText;
    public Text highScoreText;

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);

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
        Owner.ChangeState(Owner.m_sceneInGame);
        
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
