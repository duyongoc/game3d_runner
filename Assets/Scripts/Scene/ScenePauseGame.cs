using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePauseGame : StateScene
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
        Owner.ChangeState(Owner.m_sceneInGame);
    }

    public void OnPressButtonMenu()
    {
        Owner.ChangeState(Owner.m_sceneMenu);
    }

    public void OnPressButtonExit()
    {
        //Application.Quit();

        Owner.ChangeState(Owner.m_sceneGameOver);
    }
    #endregion
}
