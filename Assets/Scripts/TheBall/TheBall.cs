using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBall : MonoBehaviour
{
    // Info of player
    [Header("Data of the ball")]
    public Rigidbody m_rigidbody = default;
    public InputMobile m_inputMobile;

    [Header("All of Data for the ball need to change")]
    public Renderer ballRenderer;
    public Renderer shapeRenderer;
    public GameObject ballExplosion;
    public GameObject particleMoving;

    [Header("Set time particle when player moving")]
    public float timeParMoving = 0.5f; 

    [Header("Get data the ball from Scriptable object")]
    public ScriptTheBall scriptTheBall; 
    public float moveSpeed = 0f; 
    public float angleSpeed = 0f;

    [Header("State of the ball")]
    public BallMove m_ballMove;
    public BallPower m_ballPower;
    public BallGravity m_ballGravity;
    public BallNone m_ballNone;

    //
    public bool isFirstTriggerPower = false;

    private StateBall currentState;
    public StateBall CurrentState { get => currentState; set => currentState = value; }

    #region UNITY
    private void Start()
    {
        if(m_rigidbody == null)
            m_rigidbody = this.GetComponent<Rigidbody>();

        LoadData();
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

    private void LoadData()
    {
        this.moveSpeed = scriptTheBall.moveSpeed;
        this.angleSpeed = scriptTheBall.angleSpeed;
    }


    public void ChangeState(StateBall newState)
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

    
    public void Reset()
    {//
        //
        m_ballPower.Reset();

        isFirstTriggerPower = false;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //this.GetComponentInChildren<BallPower>().Reset();
        
        ChangeState(m_ballMove);
    }


    public bool isStateBallMove()
    {
        return currentState == m_ballMove;
    }
}
