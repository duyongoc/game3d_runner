using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StatePauseGame : S_StateBase
{
    
    public override void StartState()
    {
        base.EndState();
        this.gameObject.SetActive(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();
        this.gameObject.SetActive(false);
    }


    #region Events of button
    public void OnPressButtonResumeGame()
    {
        owner.ChangeState(owner.m_inGameScene);
    }

    public void OnPressButtonMenu()
    {
        owner.ChangeState(owner.m_menuScene);
    }

    public void OnPressButtonExit()
    {
        //Application.Quit();

        owner.ChangeState(owner.m_gameOverScene);
    }
    #endregion


}
