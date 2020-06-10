using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInGame : StateScene
{
    [Header("Tap to play in scene in game")]
    public GameObject textTapToPlay;
    public bool isPlaying = false;

    [Header("Sound background in menu game")]
    public AudioClip m_audioBackground;

    [Header("Sound when the ball get power")]
    public AudioClip m_audioPower;

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
                SoundMgr.GetInstance().PlaySound(m_audioBackground);
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
