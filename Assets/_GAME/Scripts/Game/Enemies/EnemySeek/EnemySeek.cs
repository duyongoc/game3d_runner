using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : Enemy, IDamage
{

    public enum EnemyState
    {
        Scream,
        Moving,
        Stun,
        None
    }


    [Header("[Config]")]
    public ScriptEnemySeek scriptEnemy;
    public EnemyState currentState = EnemyState.Moving;


    [Header("[Enemy's param]")]
    [SerializeField] private GameObject warningIcon;
    [SerializeField] private GameObject skinedMeshRender;


    // [ANIMATION STATE]
    private string currentAnimator;
    private const string ENEMY_SCREAM = "Enemy_Scream";
    private const string ENEMY_RUN = "Enemy_Run";
    private const string ENEMY_DEAD = "Enemy_Dead";
    private const string ENEMY_DANCE = "Enemy_Dance";

    // [private]
    private float moveSpeed = 0f;
    private float slowdownTurning = 0f;
    private float distanceWarning = 0f;
    private Vector3 veclocity = Vector3.zero;
    private Transform target;

    // explosion
    private GameObject prefabExplosion;
    private GameObject prefabExplosionSpecial;
    private GameObject prefabMoveTurning;
    private float timeTurning = 0.1f;
    private float processTurning = 0f;



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
                EnemyMoving(); break;

            case EnemyState.Stun:
                EnenmyStun(); break;

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
        {
            GetWarningFromEnemy();
        }

        SetAnimationState(ENEMY_RUN);
        Vector3 vec = new Vector3(target.position.x, 0, target.position.z);
        transform.LookAt(vec);

        //seeking the target - MC
        Vector3 distance = (target.position - transform.position);
        Vector3 desired = distance.normalized * moveSpeed;
        Vector3 steering = desired - veclocity;
        veclocity += steering * Time.deltaTime;
        transform.position += veclocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        float dot = transform.eulerAngles.y - target.eulerAngles.y;

        //check spawn trail temporary, need to research other way better
        bool hasTrail = Mathf.Abs(dot) > 40f && Mathf.Abs(dot) < 100f;
        if (hasTrail)
        {
            processTurning += Time.deltaTime;
            if (processTurning > timeTurning)
            {
                prefabMoveTurning.SpawnToGarbage(transform.position, Quaternion.identity);
                processTurning = 0;
            }
        }
    }


    private void EnenmyStun()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            prefabExplosion.SpawnToGarbage(transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }


    private void SetAnimationState(string newState)
    {
        if (mAnimator == null)
            return;

        mAnimator.Play(newState);
        currentAnimator = newState;
    }


    private void GetWarningFromEnemy()
    {
        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
        {
            warningIcon.SetActive(true);
            StartCoroutine("FinishWarningEnemySeek");
        }
    }


    private IEnumerator FinishWarningEnemySeek()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
        isWarning = false;
    }


    public void EffectFromTheHole(Transform newTarget)
    {
        target = newTarget;
        ChangeState(EnemyState.Stun);
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
        mCollider.enabled = false;
        SetAnimationState(ENEMY_DEAD);
        prefabExplosion.SpawnToGarbage(transform.localPosition, Quaternion.identity);

        ChangeState(EnemyState.None);
        StartCoroutine(Utils.DelayEvent(() => { Destroy(this.gameObject); }, 3f));
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "PlayerAbility":
                prefabExplosionSpecial.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                Destroy(gameObject);
                break;

            case "EnemyDefault":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                SelfDestroy();
                break;

            case "EnemySeek":
            case "EnemyJump":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                SelfDestroy();
                break;
        }
    }


    private void CacheDefine()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
        slowdownTurning = scriptEnemy.slowdownTurning;

        marDissolve = scriptEnemy.marDissolve;
        prefabExplosion = scriptEnemy.prefabExplosion;
        prefabExplosionSpecial = scriptEnemy.prefabExplosionSpecial;
        prefabMoveTurning = scriptEnemy.prefabMoveTurning;

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
