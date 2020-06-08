using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInGame : StateScene
{
    [Header("Tap to play in scene in game")]
    public GameObject textTapToPlay;
    public bool isPlaying = false;

    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);
        
        textTapToPlay.SetActive(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if(!isPlaying)
        {
            if(Input.GetMouseButtonDown(0))
            {
                textTapToPlay.SetActive(false);
                isPlaying = true;
            }
        }
    }

    public override void EndState()
    {
        base.EndState();

        isPlaying = false;
    }

    #region Handler event of button
    public void OnPressButtonPauseGame()
    {
        //Owner.ChangeState(Owner.m_pauseGameScene);
    }
    #endregion
    
}
