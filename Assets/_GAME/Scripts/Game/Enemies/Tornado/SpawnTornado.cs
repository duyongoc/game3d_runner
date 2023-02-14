using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTornado : Obstacle
{

    public enum SpawnState
    {
        SpawnInit,
        Spawn,
        None
    };


    [Header("[Setting]")]
    [SerializeField] private ScriptTornado scriptTornado;
    [SerializeField] private GameObject enemyPrefab;
    public List<GameObject> listEnemyCreated;
    public SpawnState currentState = SpawnState.SpawnInit;


    // [private]
    private Transform target;
    private float timeProcessDelay = 0f;
    private float timeDelay = 0f;
    private bool isStart = false;
    private float minRangeSpawn;
    private float maxRangeSpawn;

    private float timeSpawn;
    private float timerProcessSpawn;



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    private void Update()
    {
        if (!isStart && GameMgr.Instance.IsGameRunning)
        {
            timeProcessDelay += Time.deltaTime;
            if (timeProcessDelay >= timeDelay)
            {
                isStart = true;
                timeProcessDelay = 0;
            }
        }

        if (isStart && GameMgr.Instance.IsGameRunning)
        {
            switch (currentState)
            {
                case SpawnState.SpawnInit:
                    SpawnEnemyInit(); break;

                case SpawnState.Spawn:
                    SpawnEnemy(); break;

                case SpawnState.None:
                    break;
            }
        }
    }
    #endregion



    private void SpawnEnemyInit()
    {
        currentState = SpawnState.Spawn;
    }


    private void SpawnEnemy()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
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
        return new Vector3(hit.position.x, 0, hit.position.z);
    }


    public override void Reset()
    {
        foreach (GameObject obj in listEnemyCreated)
        {
            if (obj != null)
                Destroy(obj);
        }

        isStart = false;
        currentState = SpawnState.SpawnInit;

        timeProcessDelay = 0;
        timerProcessSpawn = 0;
    }


    private void CacheDefine()
    {
        timeSpawn = scriptTornado.timeSpawn;
        timerProcessSpawn = scriptTornado.timeProcessSpawn;
        timeDelay = scriptTornado.timeDelay;

        minRangeSpawn = scriptTornado.minRangeSpawn;
        maxRangeSpawn = scriptTornado.maxRangeSpawn;
    }


    private void CacheComponent()
    {
        listEnemyCreated = new List<GameObject>();
        target = MainCharacter.Instance.GetTransform();
    }

}
