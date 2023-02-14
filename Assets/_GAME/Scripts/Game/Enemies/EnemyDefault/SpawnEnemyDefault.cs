using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyDefault : SpawnEnemy, ISpawnObject
{

    public enum SpawnState
    {
        Init,
        Spawn,
        None
    };


    [Header("[CONFIG]")]
    [SerializeField] private ScriptEnemyDefault scriptEnemy;
    [SerializeField] private GameObject enemyPrefab;
    public SpawnState currentState;


    // [private]
    private List<GameObject> listEnemiesCreated;
    private Transform target;

    private float timerRemainSpawn = 0f;
    private float moveSpeed;
    private float timeToSpawn;

    private float minRangeSpawn;
    private float maxRangeSpawn;
    private bool isWarning = false;



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            switch (currentState)
            {
                case SpawnState.Init:
                    InitSpawnWarningEnemy(); break;
                case SpawnState.Spawn:
                    SpawnEnemy(); break;
                case SpawnState.None:
                    break;
            }
        }
    }
    #endregion



    private void InitSpawnWarningEnemy()
    {
        if (!isWarning)
        {
            CreateEnemyWarning(true, new Vector3(0, 0, 10));
            CreateEnemyWarning(true, new Vector3(5, 0, 20));
            CreateEnemyWarning(true, new Vector3(-5, 0, 20));
            currentState = SpawnState.Spawn;
            return;
        }

        CreateEnemyWarning(true, new Vector3(0, 0, 10));
        CreateEnemyWarning(true, new Vector3(5, 0, 20));
        CreateEnemyWarning(true, new Vector3(-5, 0, 20));
        isWarning = false;
        currentState = SpawnState.Spawn;
    }


    private void SpawnEnemy()
    {
        timerRemainSpawn += Time.deltaTime;
        if (timerRemainSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity, transform.parent);

            listEnemiesCreated.Add(obj);
            timerRemainSpawn = 0;
        }
    }


    private void CreateEnemyWarning(bool warn, Vector3 vec)
    {
        GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity, transform.parent);
        obj.GetComponent<Enemy>().SetWarning(warn);
        listEnemiesCreated.Add(obj);
    }


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
        foreach (GameObject obj in listEnemiesCreated)
        {
            if (obj != null)
                Destroy(obj);
        }

        moveSpeed = 0;
        timerRemainSpawn = 0f;
        listEnemiesCreated.Clear();

        timeToSpawn = scriptEnemy.timeSpawn;
        currentState = SpawnState.Init;
    }


    public void SetInPhaseObject(bool active, float speed = 0, float spawn = 0)
    {
        gameObject.SetActive(active);
        moveSpeed += speed;
        timeToSpawn = spawn;
    }


    private void CacheDefine()
    {
        isWarning = (scriptEnemy.setWarning == SetUp.Warning.Enable) ? true : false;
        listEnemiesCreated = new List<GameObject>();

        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;

        moveSpeed = scriptEnemy.moveSpeed;
        timeToSpawn = scriptEnemy.timeSpawn;
    }


    private void CacheComponent()
    {
        target = MainCharacter.Instance.GetTransform();
    }

}
