using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SceneShop : StateScene
{
    
    [Header("Position original")]
    [SerializeField] private int m_originX = default;
    [SerializeField] private int m_originY = default;

    [Tooltip("Speed time duration when change scene")]
    [SerializeField] private float m_speedDuration = 0.5f;


    public override void StartState()
    {
        base.EndState();
        Owner.SetActivePanelScene(this.name);
        
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
    public void OnPressButtonBackMenu()
    {
        Owner.ChangeState(Owner.m_sceneMenu);
        
        //
        owner.m_sceneShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(m_originX, m_originY), m_speedDuration);
        owner.m_sceneMenu.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, m_speedDuration);
    }
    #endregion

}
