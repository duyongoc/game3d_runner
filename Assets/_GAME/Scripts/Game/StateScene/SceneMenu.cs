using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Doozy.Engine.UI;

public class SceneMenu : StateScene
{

    [Header("[UI]")]
    [SerializeField] private UIButton btPlay;
    [SerializeField] private UIButton btnQuit;
    
        
    #region UNTIY
    private void Start()
    {
        btPlay.OnClick.OnTrigger.Event.AddListener(OnClickButtonPlay);
        btnQuit.OnClick.OnTrigger.Event.AddListener(OnClickButtonExit);
    }

    // private void Update()
    // {
    // }
    #endregion



    public void OnClickButtonPlay()
    {
    }

    public void OnClickButtonExit()
    {
        Application.Quit();
    }

}
