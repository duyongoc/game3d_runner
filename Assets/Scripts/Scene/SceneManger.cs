using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManger : MonoBehaviour
{
    //Panel of Scene
    public S_StateMenu m_menuScene ;
    public S_StateTutorial m_tutorialScene;
    public S_StateInGame m_inGameScene;
    public S_StateGameOver m_gameOverScene;
    public S_StateSetting m_settingScene;
    public S_StatePauseGame m_pauseGameScene;
    
    private S_StateBase currentState; 
    public S_StateBase CurrentState { get => currentState; set => currentState = value; }

    #region Init
    public static SceneManger s_instance;
    private void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;
    }
    #endregion

    private void Start()
    {
        ChangeState(m_menuScene);
    }

    private void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }

    public void ChangeState(S_StateBase newState)
    {
        if(currentState != null)
        {
            currentState.EndState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.owner = this;
            currentState.StartState();
        }
    }


    public void SetActivePanelScene(string panelName)
    {
        m_menuScene.gameObject.SetActive(panelName.Contains(m_menuScene.name));
        m_tutorialScene.gameObject.SetActive(panelName.Contains(m_tutorialScene.name));
        m_inGameScene.gameObject.SetActive(panelName.Contains(m_inGameScene.name));
        m_gameOverScene.gameObject.SetActive(panelName.Contains(m_gameOverScene.name));
        m_settingScene.gameObject.SetActive(panelName.Contains(m_settingScene.name));
        m_pauseGameScene.gameObject.SetActive(panelName.Contains(m_pauseGameScene.name));
    }

    public static SceneManger GetInstance()
    {
        return s_instance;
    }

    public bool IsInGame()
    {
        return currentState == m_inGameScene;
    }
}
