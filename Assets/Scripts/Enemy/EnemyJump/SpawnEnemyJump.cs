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

    private float minRangeSpawn;
    private float maxRangeSpawn;
    
    private float timeToSpawn;
    private float timerProcessSpawn;

    //
    private float timeProcessDelay = 0f;
    private float timeDelay = 0f;
    private bool isStart = false;

    //enemy's target
    private Transform target;
    public bool isWarning = false;
    public List<GameObject> enemyWasCreated;

    public enum SpawnState { First, Warning, Spawn, None };
    private SpawnState currentState;

    private void LoadData()
    {
        //
        timeToSpawn = scriptEnemy.timeSpawn;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;

        //
        timeDelay = scriptEnemy.timeDelay;

        //
        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;
    }

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
                case SpawnState.First:
                {
                    CreateFirstEnemy();
                    currentState = SpawnState.Warning;
                    break;
                }
                case SpawnState.Warning:
                {
                    if(isWarning)
                    {
                        currentState = SpawnState.Spawn;
                        break;
                    }
                    // spawn enemy don't warning yet
                    SpawnEnemy(false, GetRandomPoint());
                    break;
                }
                case SpawnState.Spawn:
                {   
                    // warning will not show up  
                    if(!isWarning)
                    {
                        foreach (GameObject obj in enemyWasCreated)
                        {
                            if (obj != null)
                                obj.GetComponent<EnemyJump>().OnSetWarning(true);
                        }
                        isWarning = true;
                    }

                    SpawnEnemy(true, GetRandomPoint());
                    break;
                }
                case SpawnState.None:
                {
                    break;
                }
            }
        }
    }

    private void CreateFirstEnemy()
    {
        if(isWarning)
        {
            GameObject obj = Instantiate(enemyPrefab, new Vector3(0,0,40), Quaternion.identity);
            obj.GetComponent<EnemyJump>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj);
        }
        else
        {
            GameObject obj = Instantiate(enemyPrefab, new Vector3(0,0,40), Quaternion.identity);
            obj.GetComponent<EnemyJump>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj);
        }
    }

    public void FinishWarningAlert()
    {
        timerProcessSpawn = 0;
        currentState = SpawnState.Spawn;
    }

    private void SpawnEnemy(bool warning, Vector3 vec)
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            obj.GetComponent<EnemyJump>().OnSetWarning(warning);

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

        timerProcessSpawn = scriptEnemy.timeProcessSpawn;
        currentState = SpawnState.First;
        timeProcessDelay = 0;

        isStart = false;
    }
}
