using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : Enemy, IDamage
{

    //
    //= public
    [Header("Config Enemy")]
    public ScriptEnemyDefault scriptEnemy;
    public EnemyState currentState = EnemyState.Moving;
    public enum EnemyState { Scream, Moving, Stun, None }


    //
    //= inspector
    [Header("Enemy's param")]
    [SerializeField] private GameObject warningIcon;
    [SerializeField] private GameObject skinedMeshRender;

    // ANIMATION STATE
    private string currentAnimator;
    private const string ENEMY_SCREAM = "Enemy_Scream";
    private const string ENEMY_RUN = "Enemy_Run";
    private const string ENEMY_DEAD = "Enemy_Dead";
    private const string ENEMY_DANCE = "Enemy_Dance";


    //
    //= private
    private float distanceWarning = 0;
    private float moveSpeed = 0f;
    private Transform target;

    //explosion
    private GameObject prefabExplosion;
    private GameObject prefabExplosionSpecial;



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
        Init();
    }

    private void Update()
    {
        if (!GameMgr.Instance.IsGameRunning)
            return;

        switch (currentState)
        {
            case EnemyState.Scream:
                break;

            case EnemyState.Moving:
                EnemyMoving();
                break;

            case EnemyState.Stun:
                EnenmyStun();
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
        if (isWarning)
            GetWarningFromEnemy();


        SetAnimationState(ENEMY_RUN);
        Vector3 vecDir = new Vector3(target.position.x, 0, target.position.z);

        transform.LookAt(vecDir);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

    private void EnenmyStun()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private void SetAnimationState(string newState)
    {
        if (currentAnimator == newState)
            return;

        mAnimator.Play(newState);
        currentAnimator = newState;
    }

    private void GetWarningFromEnemy()
    {
        if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
        {
            warningIcon.SetActive(true);
            StartCoroutine("FinishWarningEnemyDefault");
        }
    }


    IEnumerator FinishWarningEnemyDefault()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
        isWarning = false;
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

            case "EnemyDefault":
                other.GetComponent<IDamage>()?.TakeDamage(0);
                SelfDestroy();
                break;

            case "EnemySeek":
            case "PlayerAbility":
                SelfDestroy();
                break;
        }
    }


    private void CacheDefine()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
        marDissolve = scriptEnemy.marDissolve;
        prefabExplosion = scriptEnemy.prefabExplosion;
        prefabExplosionSpecial = scriptEnemy.prefabExplosionSpecial;

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
