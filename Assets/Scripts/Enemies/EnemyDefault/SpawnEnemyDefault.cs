using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyDefault : MonoBehaviour
{
    [Header("Load data of Enemy Default")]
    public ScriptEnemyDefault scriptEnemy;

    [Header("Enemies will be create")]
    public GameObject enemyPrefab;

    [Header("State of enemy")]
    public SpawnState currentState;
    public enum SpawnState { Init, Spawn, None };

    [Header("Enemy's target")]
    public  Transform target;
    public List<GameObject> enemyWasCreated;

    // private variable data
    private float minRangeSpawn;
    private float maxRangeSpawn;
    private float timeToSpawn;
    private float timerProcessSpawn;

    private bool isWarning = false;


    private void LoadData()
    {
        //set up warning from enemy
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;

        timeToSpawn = scriptEnemy.timeSpawn;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;

        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;
    }

    #region UNITY
    private void Start()
    {
        LoadData();
        target = TransformTheBall.GetInstance().GetTransform();
    }

    private void Update()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            switch (currentState)
            {
                case SpawnState.Init:
                {
                    InitSpawnWarningEnemy();
                    break;
                }
                case SpawnState.Spawn:
                {   
                    SpawnEnemy();
                    break;
                }
                case SpawnState.None:
                {
                    break;
                }
            }
        }
    }
    #endregion

    #region Function of State
    private void InitSpawnWarningEnemy()
    {
        if(!isWarning)
        {
            CreateEnemyWarning(false, new Vector3(0,0,40));
            CreateEnemyWarning(false, new Vector3(20,0,10));
            CreateEnemyWarning(false, new Vector3(-20,0,10));
            currentState = SpawnState.Spawn;
            return;
        }

        CreateEnemyWarning(true, new Vector3(0,0,40));
        CreateEnemyWarning(true, new Vector3(20,0,10));
        CreateEnemyWarning(true, new Vector3(-20,0,10));
        isWarning = false;
        currentState = SpawnState.Spawn;
    }

    #endregion

    private void CreateEnemyWarning( bool warn, Vector3 vec)
    {
        GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
        obj.GetComponent<EnemyDefault>().SetWarning(warn);
        enemyWasCreated.Add(obj);
    }

    private void SpawnEnemy()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);
            enemyWasCreated.Add(obj);
            
            timerProcessSpawn = 0;
        }
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

    public void Reset()
    {
        foreach (GameObject obj in enemyWasCreated)
        {
            if (obj != null)
                Destroy(obj);
        }

        timerProcessSpawn = 2.5f;
        currentState = SpawnState.Init;
    }
}
