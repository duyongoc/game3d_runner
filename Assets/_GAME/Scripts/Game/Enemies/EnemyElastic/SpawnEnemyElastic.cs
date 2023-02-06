using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyElastic : SpawnEnemy, ISpawnObject
{

    public enum SpawnState
    {
        Spawn,
        None
    };


    [Header("[CONFIG]")]
    [SerializeField] private ScriptEnemyElastic scriptEnemy;
    [SerializeField] private GameObject enemyPrefab;
    public SpawnState currentState = SpawnState.Spawn;


    // [private]
    private List<GameObject> listEnemyCreated;
    private Transform target;
    private float minRangeSpawn;
    private float maxRangeSpawn;

    private float timeCountDelay = 0f;
    private float timeDelay = 0f;
    private bool isStart = false;

    private float moveSpeed;
    private float timeToSpawn;
    private float timerProcessSpawn = 0;



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
                case SpawnState.Spawn:
                    StateSpawn(); break;

                case SpawnState.None: break;
            }
        }
    }
    #endregion



    private void StateSpawn()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity, transform);
            listEnemyCreated.Add(obj);
            timerProcessSpawn = 0;
        }
    }


    public void FinishWarningAlert()
    {
        timerProcessSpawn = 0;
        currentState = SpawnState.Spawn;
    }


    private Vector3 GetRandomPoint()
    {
        UnityEngine.AI.NavMeshHit hit;
        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxRangeSpawn;
            randomDirection += target.position;
            UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, maxRangeSpawn, 1);

        }
        // check the point was created isn't near player which range minRangeSpawn
        while ((Vector3.SqrMagnitude(hit.position - target.position) <= minRangeSpawn * minRangeSpawn)
            || (hit.position.x == Mathf.Infinity && hit.position.y == Mathf.Infinity)
        );

        //Debug.Log(Vector3.Distance(hit.position,target.position) + " / " + minRangeSpawn);
        Vector3 vec = new Vector3(hit.position.x, 0, hit.position.z);
        return vec;
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

        timeToSpawn = scriptEnemy.timeToSpawn;
        timeDelay = scriptEnemy.timeDelay;
        currentState = SpawnState.Spawn;
    }


    public void SetInPhaseObject(bool active, float speed = 0, float spawn = 0)
    {
        gameObject.SetActive(active);
        moveSpeed += speed;
        timeToSpawn = spawn;
    }


    private void CacheDefine()
    {
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
