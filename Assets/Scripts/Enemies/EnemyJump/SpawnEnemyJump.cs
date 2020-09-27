using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyJump : MonoBehaviour
{
    [Header("Load data Enemy Jump")]
    public ScriptEnemyJump scriptEnemy;

    [Header("Enemies will be create")]
    public GameObject enemyPrefab;

    [Header("Enemy'ss ")]
    private Transform target;
    public List<GameObject> enemyWasCreated;

    [Header("State of spawn enemy")]
    public SpawnState currentState;
    public enum SpawnState { SpawnWarning, Spawn, None };

    private float minRangeSpawn;
    private float maxRangeSpawn;
    
    private float timeToSpawn;
    private float timerProcessSpawn;

    // create enemy after time
    private float timeProcessDelay = 0f;
    private float timeDelay = 0f;
    private bool isStart = false;

    private bool isWarning = false;
    private int numberOfWarning = 0;

    private void LoadData()
    {
        //set up warning from enemy
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;
        numberOfWarning = scriptEnemy.numberOfWarning;

        timeToSpawn = scriptEnemy.timeSpawn;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;

        timeDelay = scriptEnemy.timeDelay;
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
        if(!isStart)
        {
            timeProcessDelay += Time.deltaTime;
            if(timeProcessDelay >= timeDelay)
            {
                isStart = true;
                timeProcessDelay = 0;
            }
        }
        
        if (isStart && SceneMgr.GetInstance().IsStateInGame())
        {
            switch (currentState)
            {
                // case SpawnState.Init:
                // {
                //     InitSpawn();
                //     break;
                // }
                case SpawnState.SpawnWarning:
                {
                    SpawnEnenyWithWarning();
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
            // Debug.Log(currentState);
        }
    }
    #endregion

    #region Function of State Enemy
    // private void InitSpawn()
    // {
    //     GameObject obj = Instantiate(enemyPrefab, new Vector3(0,0,40), Quaternion.identity);
    //     obj.GetComponent<EnemyJump>().SetWarning(isWarning);
    //     enemyWasCreated.Add(obj);
    // }

    private void SpawnEnenyWithWarning()
    {
        if( !isWarning)
        {
            currentState = SpawnState.Spawn;
            return;
        }

        timerProcessSpawn += Time.deltaTime;
        if ( numberOfWarning >= 0 && timerProcessSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);
            obj.GetComponent<EnemyJump>().SetWarning(true);
            enemyWasCreated.Add(obj);
            
            numberOfWarning--;
            if(numberOfWarning == 0)
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
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);
            enemyWasCreated.Add(obj);

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

    public void Reset()
    {
        foreach (GameObject obj in enemyWasCreated)
        {
            if (obj != null)
                Destroy(obj);
        }

        numberOfWarning = scriptEnemy.numberOfWarning;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;
        currentState = SpawnState.SpawnWarning;

        timeProcessDelay = 0;
        isStart = false;
    }
}
