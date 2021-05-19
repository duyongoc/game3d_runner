using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyJump : SpawnEnemy, ISpawnObject
{

    //
    //= inspector
    [Header("CONFIG")]
    [SerializeField] private ScriptEnemyJump scriptEnemy;
    [SerializeField] private GameObject enemyPrefab;
    public SpawnState currentState;
    public enum SpawnState { SpawnWarning, Spawn, None };


    //
    //= private
    private Transform target;
    private List<GameObject> listEnemyCreated;
    private float minRangeSpawn;
    private float maxRangeSpawn;

    private bool isWarning = false;
    private bool isStart = false;
    private float timeDelay = 0f;
    private float timeCountDelay = 0f;

    private float moveSpeed;
    private float timeToSpawn;
    private int numberOfWarning = 0;
    private float timerProcessSpawn = 0;


    public static SpawnEnemyJump Instance;

    #region UNITY
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    private void Update()
    {
        if (!GameMgr.Instance.IsGameRunning)
            return;

        if (!isStart)
        {
            timeCountDelay += Time.deltaTime;
            if (timeCountDelay >= timeDelay)
            {
                isStart = true;
                timeCountDelay = 0;
            }
        }

        if (isStart)
        {
            switch (currentState)
            {
                case SpawnState.SpawnWarning:
                    SpawnEnenyWithWarning();
                    break;

                case SpawnState.Spawn:
                    SpawnEnemy();
                    break;

                case SpawnState.None:
                    break;
            }
        }

    }
    #endregion

    #region Function of State Enemy
    // private void InitSpawn()
    // {
    //     GameObject obj = Instantiate(enemyPrefab, new Vector3(0,0,40), Quaternion.identity);
    //     obj.GetComponent<EnemyJump>().SetWarning(isWarning);
    //     listEnemyCreated.Add(obj);
    // }

    private void SpawnEnenyWithWarning()
    {
        if (!isWarning)
        {
            currentState = SpawnState.Spawn;
            return;
        }

        timerProcessSpawn += Time.deltaTime;
        if (numberOfWarning >= 0 && timerProcessSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity, transform);
            obj.GetComponent<Enemy>().SetWarning(true);
            listEnemyCreated.Add(obj);

            numberOfWarning--;
            if (numberOfWarning == 0)
            {
                isWarning = false;
            }
            timerProcessSpawn = 0;
        }


    }

    private void SpawnEnemy()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity, transform);
            listEnemyCreated.Add(obj);

            timerProcessSpawn = 0;
        }
    }
    #endregion

    private Vector3 GetRandomPoint()
    {
        NavMeshHit hit;
        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxRangeSpawn;
            randomDirection += target.position;
            NavMesh.SamplePosition(randomDirection, out hit, maxRangeSpawn, 1);

        }
        // check the point was created isn't near player which range minRangeSpawn
        while ((Vector3.SqrMagnitude(hit.position - target.position) <= minRangeSpawn * minRangeSpawn)
            || (hit.position.x == Mathf.Infinity && hit.position.y == Mathf.Infinity)
        );

        //Debug.Log(Vector3.Distance(hit.position,target.position) + " / " + minRangeSpawn);
        Vector3 vec = new Vector3(hit.position.x, 0, hit.position.z);
        return vec;
    }

    public void SetInPhaseObject(bool active, float speed = 0, float spawn = 0)
    {
        this.gameObject.SetActive(active);
        moveSpeed += speed;
        timeToSpawn = spawn;
    }

    public void AddEnemyCreated(GameObject ene)
    {
        listEnemyCreated.Add(ene);
    }

    private void RemoveListEnemy()
    {
        if (listEnemyCreated == null)
            return;

        foreach (GameObject obj in listEnemyCreated)
        {
            if (obj != null)
                Destroy(obj);
        }

        listEnemyCreated.Clear();
    }

    public override void Reset()
    {
        RemoveListEnemy();
        
        isStart = false;
        moveSpeed = 0;
        timeCountDelay = 0;
        timerProcessSpawn = 0;

        timeDelay = scriptEnemy.timeDelay;
        timeToSpawn = scriptEnemy.timeToSpawn;
        currentState = SpawnState.SpawnWarning;
        numberOfWarning = scriptEnemy.numberOfWarning;
    }


    private void CacheDefine()
    {
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;
        numberOfWarning = scriptEnemy.numberOfWarning;

        timeDelay = scriptEnemy.timeDelay;
        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;

        moveSpeed = scriptEnemy.moveSpeed;
        timeToSpawn = scriptEnemy.timeToSpawn;
    }

    private void CacheComponent()
    {
        target = MainCharacter.Instance.GetTransform();
        listEnemyCreated = new List<GameObject>();
    }

}
