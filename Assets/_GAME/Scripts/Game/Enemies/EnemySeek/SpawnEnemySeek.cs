using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemySeek : SpawnEnemy, ISpawnObject
{

    //
    //= inspector
    [Header("CONFIG")]
    [SerializeField] private ScriptEnemySeek scriptEnemy;
    [SerializeField] private GameObject enemyPrefab;
    public SpawnState currentState;
    public enum SpawnState { Warning, Spawn, None };


    //
    //= private
    private List<GameObject> listEnemyCreated;
    private Transform target;

    private float timeProcessDelay = 0f;
    private float timeDelay = 0f;
    private bool isStart = false;
    private bool isWarning = false;
    private int numberOfWarning = 0;

    private float minRangeSpawn;
    private float maxRangeSpawn;
    private float moveSpeed;
    private float timeToSpawn;
    private float timerProcessSpawn;


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    private void Update()
    {
        if (!GameMgr.Instance.IsGameRunning)
            return;

        // spawn enemy after time delay
        if (!isStart)
        {
            timeProcessDelay += Time.deltaTime;
            if (timeProcessDelay >= timeDelay)
            {
                isStart = true;
                timeProcessDelay = 0;
            }
        }

        // spawning enemy with state 
        if (isStart)
        {
            switch (currentState)
            {
                case SpawnState.Warning:
                    SpawnEnemyWarning();
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

    #region Function of State
    private void SpawnEnemyWarning()
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
            obj.GetComponent<EnemyGlobe>().SetWarning(true);
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

    public override void Reset()
    {
        foreach (GameObject obj in listEnemyCreated)
        {
            if (obj != null)
                Destroy(obj);
        }
        listEnemyCreated.Clear();

        moveSpeed = 0;
        isStart = false;
        timeProcessDelay = 0f;
        timerProcessSpawn = 0f;

        timeDelay = scriptEnemy.timeDelay;
        timeToSpawn = scriptEnemy.timeSpawn;
        currentState = SpawnState.Spawn;
    }

    public void SetInPhaseObject(bool active, float speed = 0, float spawn = 0)
    {
        this.gameObject.SetActive(active);
        moveSpeed += speed;
        timeToSpawn = spawn;
    }


    private void CacheDefine()
    {
        //set up warning from enemy
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;
        timeDelay = scriptEnemy.timeDelay;
        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;

        timeToSpawn = scriptEnemy.timeSpawn;
        numberOfWarning = scriptEnemy.numberOfWarning;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;
    }

    private void CacheComponent()
    {
        target = MainCharacter.Instance.GetTransform();
        listEnemyCreated = new List<GameObject>();
    }

}
