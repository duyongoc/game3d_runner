using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    [Header("All of scene in game")]
    public SceneMenu m_sceneMenu;
    public SceneInGame m_sceneInGame;
    public SceneMission m_sceneMission;
    public SceneShop m_sceneShop;
    public SceneGameOver m_sceneGameOver;

    //current state scene
    private StateScene currentState; 
    public StateScene CurrentState { get => currentState; set => currentState = value; }

    #region Init
    public static SceneMgr s_instance;
    private void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;
    }
    #endregion

    private void Start()
    {
        ChangeState(m_sceneMenu);
    }

    private void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }

    public void ChangeState(StateScene newState)
    {
        if(currentState != null)
        {
            currentState.EndState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.Owner = this;
            currentState.StartState();
        }
    }


    public void SetActivePanelScene(string panelName)
    {
        m_sceneMenu.gameObject.SetActive(panelName.Contains(m_sceneMenu.name));
        m_sceneInGame.gameObject.SetActive(panelName.Contains(m_sceneInGame.name));
        m_sceneMission.gameObject.SetActive(panelName.Contains(m_sceneMission.name));
        m_sceneShop.gameObject.SetActive(panelName.Contains(m_sceneShop.name));
        m_sceneGameOver.gameObject.SetActive(panelName.Contains(m_sceneGameOver.name));

    }

    public static SceneMgr GetInstance()
    {
        return s_instance;
    }

    public bool IsInGame()
    {
        return currentState == m_sceneInGame;
    }

}
