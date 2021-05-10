using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : Singleton<MainCharacter>
{

    //
    //= public
    [Header("CONFIG")]
    public CharacterConfigSO CONFIG_CHARACTER;


    //
    //= inspector
    [Header("Character's param")]
    [SerializeField] private VirtualMovement virtualMovement;
    [SerializeField] private Transform[] characterFeet;
    [SerializeField] private Renderer shapeRenderer;
    

    //
    //= private
    private Rigidbody mRigidbody;
    private Animator animator;
    private CharacterMove mCharacterMove;
    private CharacterHolding mCharacterHolding;
    private CharacterAbility mCharacterAbility;
    private CharacterNone mCharacterNone;

    private StateCharacter currentState;
    private float moveSpeed = 0f;
    private float angleSpeed = 0f;

    private float timeParMoving = 0.5f;
    private float timeProcessFinish = 0f;
    private float currentTimeProcess = 0;
    private float speedIncrease = 0f;
    private float timerSpeedUp = 0f;
    private float engineForce = 0f;
    private float turnSpeed = 0f;

    private bool firstTriggerPower = false;
    private GameObject particleMoving;


    //
    //= properties
    public Rigidbody GetRigidbody { get => mRigidbody; set => mRigidbody = value; }
    public Animator GetAnimator { get => animator; }
    public StateCharacter CurrentState { get => currentState; set => currentState = value; }
    public CharacterMove GetCharacterMove { get => mCharacterMove; }
    public CharacterHolding GetCharacterHolding { get => mCharacterHolding; }
    public CharacterAbility GetCharacterAbility { get => mCharacterAbility; }
    public CharacterNone GetCharacterNone { get => mCharacterNone; }
    public Transform[] GetCharacterFeet { get => characterFeet; }

    public float GetMoveSpeed { get => moveSpeed; }
    public float GetAngleSpeed { get => angleSpeed; }
    public float GetTimeProcessFinish { get => timeProcessFinish; }
    public float GetCurrentTimeProcess { get => currentTimeProcess; }
    public float GetSpeedIncrease { get => speedIncrease; }
    public float GetTimerSpeedUp { get => timerSpeedUp; }
    public float GetEngineForce { get => engineForce; }
    public float GetTurnSpeed { get => turnSpeed; }
    public float GetTimeParMoving { get => timeParMoving; }

    public VirtualMovement VirtualMovement { get => virtualMovement;}
    public GameObject GetParticleMoving { get => particleMoving; }
    public bool FirstTriggerPower { get => firstTriggerPower; set => firstTriggerPower = value; }



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();

        ChangeState(mCharacterMove);
    }

    private void FixedUpdate()
    {
        if (currentState == null || !GameMgr.Instance.IsGameRunning)
            return;

        currentState.UpdateState();
    }
    #endregion


    public void ChangeState(StateCharacter newState)
    {
        if (currentState != null)
        {
            currentState.EndState();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.StartState();
        }
    }


    public void SetPlayerDead()
    {
        this.GetComponent<Collider>().enabled = false;
        this.animator.SetBool("Dead", true);
        this.ChangeState(this.mCharacterMove);
    }

    public void Reset()
    {//
        //
        mCharacterMove.Reset();

        firstTriggerPower = false;
        animator.SetBool("Moving", false);
        animator.SetBool("Dead", false);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        ChangeState(mCharacterMove);
    }


    public bool IsStateMove()
    {
        return currentState == mCharacterMove;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }

    public void IncreaseSpeed()
    {
        moveSpeed += speedIncrease;
    }

    public void ResetSpeed()
    {
        moveSpeed = CONFIG_CHARACTER.moveSpeed;
        angleSpeed = CONFIG_CHARACTER.angleSpeed;
    }


    private void CacheDefine()
    {
        moveSpeed = CONFIG_CHARACTER.moveSpeed;
        angleSpeed = CONFIG_CHARACTER.angleSpeed;

        speedIncrease = CONFIG_CHARACTER.speedIncrease;
        timerSpeedUp = CONFIG_CHARACTER.timerSpeedUp;
        engineForce = CONFIG_CHARACTER.engineForce;
        turnSpeed = CONFIG_CHARACTER.turnSpeed;

    }

    private void CacheComponent()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mCharacterMove = GetComponent<CharacterMove>();
        mCharacterHolding = GetComponent<CharacterHolding>();
        mCharacterAbility = GetComponent<CharacterAbility>();
        mCharacterNone = GetComponent<CharacterNone>();

    }

}
