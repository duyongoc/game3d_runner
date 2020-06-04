using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StateMenu : S_StateBase
{


    public override void StartState()
    {
        base.StartState();
        owner.SetActivePanelScene(this.name);
    }

    public override void UpdateState()
    {

    }

    public override void EndState()
    {

    }


    #region Handler events of button
    public void OnPressButtonPlay()
    {
        owner.ChangeState(owner.m_inGameScene);
    }

    public void OnPressButtonSetting()
    {
        owner.ChangeState(owner.m_settingScene);
    }

    public void OnPressButtonExit()
    {
        Application.Quit();
    }
    
    #endregion
}
