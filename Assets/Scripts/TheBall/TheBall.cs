using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBall : MonoBehaviour
{
    // Info of player
    [Header("Data of the ball")]
    public Rigidbody m_rigidbody = default;
    public InputMobile m_inputMobile;

    [Header("Explosion of the ball when dead")]
    public GameObject ballExplosion;

    [Header("Particle when the ball moving")]
    public GameObject particleMoving;
    public float timeParMoving = 0.5f;

    [Header("Get data the ball from Scriptable object")]
    public ScriptTheBall scriptTheBall; 
    public float moveSpeed = 0f; 
    public float angleSpeed = 0f;

    //
    [Header("State of the ball")]
    public BallMove m_ballMove;
    public BallNone m_ballNone;
    public BallGravity m_ballGravity;

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
            newState.EndState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.Owner = this;
            currentState.StartState();
        }
    }

    
    public void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        this.GetComponentInChildren<BallPower>().Reset();
        
        //
        ChangeState(m_ballMove);
    }
}
