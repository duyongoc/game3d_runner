using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StateInGame : S_StateBase
{
    
    public override void StartState()
    {
        base.EndState();
        owner.SetActivePanelScene(this.name);
        
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

    }

    #region Handler event of button
    public void OnPressButtonPauseGame()
    {
        owner.ChangeState(owner.m_pauseGameScene);
    }
    #endregion

}
