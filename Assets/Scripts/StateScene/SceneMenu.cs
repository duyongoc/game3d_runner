using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneMenu : StateScene
{
    [Header("Position original")]
    [SerializeField] private int m_originX = default;
    [SerializeField] private int m_originY = default;

    [Tooltip("Speed time duration when change scene")]
    [SerializeField] private float m_speedDuration = 0.5f;
    

    public override void StartState()
    {
        base.StartState();
        
        //Set up when game start
        owner.SetActivePanelScene(this.name);
        owner.m_sceneMenu.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0);
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
        Owner.ChangeState(Owner.m_sceneInGame);
        
        //ingame
        owner.m_sceneMenu.GetComponent<RectTransform>().DOAnchorPos(new Vector2(m_originX, m_originY), m_speedDuration);

    }

    public void OnPressButtonShop()
    {
        Owner.ChangeState(Owner.m_sceneShop);
        
        //shop
        //owner.m_sceneMenu.GetComponent<RectTransform>().DOAnchorPos(new Vector2(m_originX, m_originY), m_speedDuration * 2);
        owner.m_sceneShop.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, m_speedDuration);
    }

    public void OnPressButtonMission()
    {
        owner.ChangeState(owner.m_sceneMission);

        //mission
        owner.m_sceneMenu.GetComponent<RectTransform>().DOAnchorPos(new Vector2(m_originX, m_originY), m_speedDuration * 2);
        owner.m_sceneMission.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, m_speedDuration);
    }

    public void OnPressButtonExit()
    {
        Application.Quit();
    }
    
    #endregion
}
