using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBall : MonoBehaviour
{
    // Info of player
    public Rigidbody m_rigidbody = default;
    public InputMobile m_inputMobile;
    
    //
    public BallMove m_ballMove;
    public BallNone m_ballNone;

    private StateBall currentState;
    public StateBall CurrentState { get => currentState; set => currentState = value; }

    #region UNITY
    private void Start()
    {
        if(m_rigidbody == null)
            m_rigidbody = this.GetComponent<Rigidbody>();

        ChangeState(m_ballMove);
    }

    private void FixedUpdate()
    {
        if(currentState != null)// && SceneManager.GetInstance().IsInGame())
        {
            currentState.UpdateState();
        }
    }
    #endregion


    public void ChangeState(StateBall newState)
    {
        if(currentState != null)
        {
            newState.EndState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.Owner = this;
            currentState.StartState();
        }
    }
}
