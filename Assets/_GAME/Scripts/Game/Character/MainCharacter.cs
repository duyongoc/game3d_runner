using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : Singleton<MainCharacter>
{

    // [event]
    public Action EVENT_PLAYER_DEAD;

    [Header("[CONFIG]")]
    public CharacterConfigSO CONFIG_CHARACTER;

    [Header("[Setting]")]
    [SerializeField] private VirtualMovement virtualMovement;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private Transform[] characterFeet;
    [SerializeField] private Renderer shapeRenderer;


    // [private]
    private GameObject particleMoving;
    private Rigidbody mRigidbody;
    private Animator mAnimator;
    private Collider mCollider;
    private CharacterMove mCharacterMove;
    private CharacterHolding mCharacterHolding;
    private CharacterShield mCharacterShield;
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


    // [ANIMATION STATE]
    private string currentAnimator;
    private const string CHARACTER_IDLE = "Character_Idle";
    private const string CHARACTER_RUN = "Character_Run";
    private const string CHARACTER_DEAD = "Character_Dead";


    // [properties]
    public Rigidbody GetRigidbody { get => mRigidbody; set => mRigidbody = value; }
    public Animator GetAnimator { get => mAnimator; }
    public StateCharacter CurrentState { get => currentState; set => currentState = value; }
    public CharacterMove GetCharacterMove { get => mCharacterMove; }
    public CharacterHolding GetCharacterHolding { get => mCharacterHolding; }
    public CharacterShield GetCharacterShield { get => mCharacterShield; }
    public CharacterNone GetCharacterNone { get => mCharacterNone; }
    public GameObject GetShieldEffect { get => shieldEffect; }
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

    public VirtualMovement VirtualMovement { get => virtualMovement; }
    public GameObject GetParticleMoving { get => particleMoving; }
    public bool FirstTriggerPower { get => firstTriggerPower; set => firstTriggerPower = value; }



    #region UNITY
    private void Start()
    {
        GameMgr.Instance.EVENT_RESET_INGAME += CharacterReset;
        CacheComponent();
        CacheDefine();
        Init();
    }

    private void FixedUpdate()
    {
        if (currentState == null || !GameMgr.Instance.IsGameRunning)
            return;

        currentState.UpdateState();
    }
    #endregion



    private void Init()
    {
        ChangeState(mCharacterMove);
    }


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


    public void PlayerDead()
    {
        SetAnimationDead();
        EVENT_PLAYER_DEAD?.Invoke();
        SoundMgr.PlaySoundOneShot(SoundMgr.Instance.SFX_CHARACTER_DEAD);

        mCollider.enabled = false;
        ChangeState(mCharacterNone);
    }


    private void SetAnimationState(string newState)
    {
        if (currentAnimator == newState)
            return;

        mAnimator.Play(newState);
        currentAnimator = newState;
    }


    public void SetAnimationIdle()
    {
        SetAnimationState(CHARACTER_IDLE);
    }


    public void SetAnimationMoving()
    {
        SetAnimationState(CHARACTER_RUN);
    }


    public void SetAnimationDead()
    {
        SetAnimationState(CHARACTER_DEAD);
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


    public void CharacterReset()
    {
        // firstTriggerPower = false;
        SetAnimationIdle();
        virtualMovement.Reset();
        mCollider.enabled = true;
        gameObject.ResetTransform();

        mCharacterMove.Reset();
        ChangeState(mCharacterMove);
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
        mAnimator = characterModel.GetComponent<Animator>();
        mCollider = GetComponent<Collider>();

        mCharacterMove = GetComponent<CharacterMove>();
        mCharacterHolding = GetComponent<CharacterHolding>();
        mCharacterShield = GetComponent<CharacterShield>();
        mCharacterNone = GetComponent<CharacterNone>();
    }

}
