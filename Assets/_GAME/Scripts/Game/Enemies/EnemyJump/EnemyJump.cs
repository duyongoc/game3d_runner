using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyJump : Enemy, IDamage
{

    //
    //= public
    [Header("CONFIG")]
    public ScriptEnemyJump scriptEnemy;
    public enum EnemyState { Scream, Stop, Moving, Jumping, Stun, None }
    public EnemyState currentState = EnemyState.Jumping;


    //
    //= inspector
    [Header("Enemy's param")]
    [SerializeField] private GameObject warningIcon;
    [SerializeField] private GameObject skinedMeshRender;

    // ANIMATION STATE
    private string currentAnimator;
    private const string ENEMY_SCREAM = "Enemy_Scream";
    private const string ENEMY_RUN = "Enemy_Run";
    private const string ENEMY_JUMP = "Enemy_Jump";
    private const string ENEMY_DEAD = "Enemy_Dead";
    private const string ENEMY_DANCE = "Enemy_Dance";


    //
    //= private
    private Transform target;
    private float timeWaiting = 2f;
    private float processWaiting = 0f;
    private float distanceAttack = 5f;
    private float moveSpeed = 0f;
    private float distanceWarning = 0;

    private Vector3 currentTarget;
    private GameObject prefabExplosion;
    private GameObject prefabJumpExplosion;
    private GameObject shapeArlet;
    private GameObject alertShapeBackup;


    #region UNITY
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

            case EnemyState.Jumping:
                EnemyJumping();
                break;

            case EnemyState.Stun:
                EnenmyStun();
                break;

            case EnemyState.Stop:
                EnemyStop();
                break;

            case EnemyState.Scream:
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

        StartCoroutine(Utils.DelayEvent(() =>
        {
            mCollider.enabled = true;
            ChangeState(EnemyState.Moving);
        }, 2.5f));
    }


    private void EnemyMoving()
    {
        if (isWarning)
            GetWarningFromEnemy();

        SetAnimationState(ENEMY_RUN);
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(this.transform.position, target.position) < distanceAttack)
        {
            Vector3 vec = (target.position - transform.position).normalized;
            currentTarget = target.position + vec * 3f;
            SetAlertPlacement(new Vector3(currentTarget.x, 0.5f, currentTarget.z), true);
            ChangeState(EnemyState.Jumping);
        }
    }

    private void EnemyStop()
    {
        transform.LookAt(target.position);
        processWaiting += Time.deltaTime;

        if (processWaiting > timeWaiting)
        {
            ChangeState(EnemyState.Moving);
            SetAlertPlacement(Vector3.zero, false);

            processWaiting = 0;
        }
    }

    private void EnemyJumping()
    {
        SetAnimationState(ENEMY_JUMP);

        if (gameObject != null)
            transform.DOJump(currentTarget, 5f, 1, 1.2f, false);

        ChangeState(EnemyState.Stop);
    }

    private void EnenmyStun()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            prefabExplosion.SpawnToGarbage(transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private void SetAnimationState(string newState)
    {
        if (currentAnimator == newState || mAnimator == null)
            return;

        mAnimator.Play(newState);
        currentAnimator = newState;
    }

    private void OnEventPlayerDead()
    {
        SetAnimationState(ENEMY_DANCE);
    }

    private void SetAlertPlacement(Vector3 pos, bool active)
    {
        if (alertShapeBackup == null)
        {
            alertShapeBackup = Instantiate(shapeArlet);
            SpawnEnemyJump.Instance.AddEnemyCreated(alertShapeBackup);
        }
        alertShapeBackup.SetActive(active);
        alertShapeBackup.transform.position = pos;
    }

    private void GetWarningFromEnemy()
    {
        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
        {
            warningIcon.SetActive(true);
            StartCoroutine("FinishWarningEnemyJump");
        }
    }

    IEnumerator FinishWarningEnemyJump()
    {
        yield return new WaitForSeconds(2f);
        isWarning = false;
        warningIcon.SetActive(false);
    }

    public void EffectFromTheHole(Transform newTarget)
    {
        target = newTarget;
        ChangeState(EnemyState.Stun);
    }

    public void TakeDamage(float damage)
    {
        SelfDestroy();
    }

    public void SelfDestroy()
    {
        mCollider.enabled = false;
        ChangeState(EnemyState.None);
        Destroy(alertShapeBackup.gameObject);
        prefabExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);

        SetAnimationState(ENEMY_DEAD);
        StartCoroutine(Utils.DelayEvent(() => { DestroyObject(); }, 3f));
    }

    public void DestroyObject()
    {
        Destroy(alertShapeBackup.gameObject);
        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "PlayerAbility":
                prefabJumpExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                Destroy(gameObject);
                break;

            case "EnemyDefault":
            case "EnemySeek":
            case "EnemyJump":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                SelfDestroy();
                break;

            case "AlertShape":
                FeedBackMgr.Instance.PlayFeedBack(FeedBackMgr.Instance.ENMEY_JUMP);
                prefabJumpExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                break;
        }
    }


    private void CacheDefine()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
        marDissolve = scriptEnemy.marDissolve;
        prefabExplosion = scriptEnemy.prefabExplosion;
        prefabJumpExplosion = scriptEnemy.prefabJumpExplosion;
        shapeArlet = scriptEnemy.shapeArlet;

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        SetAlertPlacement(Vector3.zero, false);
        currentTarget = target.position;
        warningIcon.SetActive(false);
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
