using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    // Info of player
    [Header("Data of the ball")]
    public Rigidbody m_rigidbody = default;
    //public InputMobile m_inputMobile;  //don't use input from user anymore

    [Header("All of Data for the ball need to change")]
    //public Renderer ballRenderer;
    public Renderer shapeRenderer;
    public GameObject ballExplosion;
    

    [Header("Animator")]
    public Animator animator;

    [Header("Set time particle when player moving")]
    public GameObject particleMoving;
    public float timeParMoving = 0.5f; 
    public Transform[] arrTransFoot;

    [Header("Get data the ball from Scriptable object")]
    public ScriptPlayer scriptPlayer; 
    public float moveSpeed = 0f; 
    public float angleSpeed = 0f;
    
    [Header("State of the ball")]
    public CharacterMove m_characterMove;
    public CharacterHolding m_characterHolding;
    public CharacterAbility m_characterAbility;
    public CharacterNone m_characterNone;

    [Header("Slider process")]
    public Slider sliderProcess;
    public Image sliderImage;
    public float timeProcessFinish = 0f;
    public float currentTimeProcess = 0;

    //
    public float speedIncrease = 0f;
    public float timerSpeedUp = 0f;
    public float engineForce = 0f;
    public float turnSpeed = 0f;

    //
    public bool isFirstTriggerPower = false;

    private StateCharacter currentState;
    public StateCharacter CurrentState { get => currentState; set => currentState = value; }

    #region UNITY
    private void Start()
    {
        if(m_rigidbody == null)
            m_rigidbody = this.GetComponent<Rigidbody>();

        LoadData();
        ChangeState(m_characterMove);
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
        this.moveSpeed = scriptPlayer.moveSpeed;
        this.angleSpeed = scriptPlayer.angleSpeed;
        //
        this.speedIncrease = scriptPlayer.speedIncrease;
        this.timerSpeedUp = scriptPlayer.timerSpeedUp;
        this.engineForce = scriptPlayer.engineForce;
        this.turnSpeed = scriptPlayer.turnSpeed;

        sliderProcess.gameObject.SetActive(false);
    }


    public void ChangeState(StateCharacter newState)
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
        m_characterMove.Reset();

        isFirstTriggerPower = false;
        animator.SetBool("Dead", false);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //this.GetComponentInChildren<BallPower>().Reset();
        
        ChangeState(m_characterMove);
    }


    public bool isStateBallMove()
    {
        return currentState == m_characterMove;
    }
}
