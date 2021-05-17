using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyElastic : Enemy, IDamage
{

    //
    //= public
    [Header("Config Enemy")]
    [Header("Load data Enemy Elastic")]
    public ScriptEnemyElastic scriptEnemy;
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Scream, Moving, Preparing, Attack, Stop, None }


    //
    //= inspector
    [Header("Enemy's param")]
    [SerializeField] private GameObject arrowAttack;
    [SerializeField] private GameObject affectAttack;
    [SerializeField] private GameObject skinedMeshRender;


    // ANIMATION STATE
    private string currentAnimator;
    private const string ENEMY_SCREAM = "Enemy_Scream";
    private const string ENEMY_IDLE = "Enemy_Idle";
    private const string ENEMY_RUN = "Enemy_Run";
    private const string ENEMY_ATTACK = "Enemy_Attack";
    private const string ENEMY_DEAD = "Enemy_Dead";
    private const string ENEMY_DANCE = "Enemy_Dance";


    // Elastic Enemy
    private Transform target;
    private float moveSpeed = 0f;
    private float distanceAttack = 5f;
    private float timeCharge = 0f;
    private float timeAttack = 0f;

    private Vector3 attackPosition;
    private bool isAttack = false;
    private bool isPrepare = false;

    //explosion
    private GameObject prefabExplosion;
    private GameObject prefabPrepareAttack;



    #region  UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
        Init();

        MainCharacter.Instance.EVENT_PLAYER_DEAD += OnEventPlayerDead;
    }

    private void Update()
    {
        if (!GameMgr.Instance.IsGameRunning)
            return;

        switch (currentState)
        {
            case EnemyState.Moving:
                EnemyMoving();
                break;

            case EnemyState.Preparing:
                EnemyPreparing();
                break;

            case EnemyState.Attack:
                EnemyAttacking();
                break;

            case EnemyState.None:
                break;
        }
    }
    #endregion


    private void Init()
    {
        EnemyAppear();
        SetAnimationState(ENEMY_SCREAM);
        ChangeState(EnemyState.Scream);

        StartCoroutine(Utils.DelayEvent(() => { ChangeState(EnemyState.Moving); }, 2.5f));
    }


    private void EnemyMoving()
    {
        SetAnimationState(ENEMY_RUN);
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(this.transform.position, target.position) < distanceAttack)
        {
            ChangeState(EnemyState.Preparing);
        }
    }

    private void EnemyPreparing()
    {
        if (!isPrepare)
        {
            arrowAttack.SetActive(true);
            SetAnimationState(ENEMY_ATTACK);
            Invoke("ChangeStateAttack", timeCharge);
            isAttack = false;
            isPrepare = true;
        }

        transform.LookAt(target.position);
    }

    private void EnemyAttacking()
    {
        if (isAttack)
            return;

        arrowAttack.SetActive(false);
        affectAttack.SetActive(true);

        attackPosition = transform.position + (transform.forward * 15f);
        transform.DOMove(attackPosition, timeAttack, false).SetEase(Ease.OutCubic);

        Invoke("ChangeStateMoving", timeAttack);
        isPrepare = false;
        isAttack = true;
    }


    private void ChangeState(EnemyState state = EnemyState.Attack)
    {
        currentState = state;
    }

    private void SetAnimationState(string newState)
    {
        if (currentAnimator == newState)
            return;

        mAnimator.Play(newState);
        currentAnimator = newState;
    }

    private void ChangeStateAttack()
    {
        currentState = EnemyState.Attack;
    }

    private void ChangeStateMoving()
    {
        currentState = EnemyState.Moving;
        affectAttack.SetActive(false);
    }

    private void ChangeStatePreparing()
    {
        currentState = EnemyState.Preparing;
    }

    public void CreateEffectPrepare()
    {
        prefabPrepareAttack.SpawnToGarbage(transform.position, Quaternion.identity);
    }

    private void OnEventPlayerDead()
    {
        SetAnimationState(ENEMY_DANCE);
    }

    public void TakeDamage(float damage)
    {
        SelfDestroy();
    }

    public void SelfDestroy()
    {
        CancelInvoke();
        mCollider.enabled = false;
        arrowAttack.SetActive(false);
        DOTween.Kill(transform);

        ChangeState(EnemyState.None);
        SetAnimationState(ENEMY_DEAD);

        prefabExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
        StartCoroutine(Utils.DelayEvent(() => { Destroy(gameObject); }, 3f));
    }


    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                prefabExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                other.gameObject.SetActive(false);
                break;

            case "PlayerAbility":
                SelfDestroy();
                break;

            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
            case "EnemyElastic":
                prefabExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "Obstacle":
                other.gameObject.GetComponent<StaticObstacle>().DissolveObstacle();
                prefabExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                break;
        }
    }


    private void CacheDefine()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceAttack = scriptEnemy.distanceAttack;
        timeCharge = scriptEnemy.timeCharge;
        timeAttack = scriptEnemy.timeAttack;

        marDissolve = scriptEnemy.marDissolve;
        prefabExplosion = scriptEnemy.prefabExplosion;
        prefabPrepareAttack = scriptEnemy.prefabPrepareAttack;

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }


    private void CacheComponent()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mAnimator = GetComponentInChildren<Animator>();
        mCollider = GetComponent<Collider>();

        target = MainCharacter.Instance.GetTransform();

        // Get materials-meshes default
        d_skinedMeshRender = new Dictionary<SkinnedMeshRenderer, Material>();
        var arrayRender = skinedMeshRender.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skin in arrayRender)
            d_skinedMeshRender.Add(skin, skin.material);
    }

}
