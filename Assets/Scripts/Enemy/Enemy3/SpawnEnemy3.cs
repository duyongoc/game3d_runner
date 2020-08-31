using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy3 : MonoBehaviour
{
    [Header("Data for Enemy 3")]
    public ScriptEnemy3 scriptEnemy3;
    
    [Header("Enemies prefab")]
    public GameObject enemyPrefab;

    private float minRangeSpawn;
    private float maxRangeSpawn ;
    
    private float timeSpawn ;
    private float timerProcessSpawn;

    //
    public float timeProcessDelay = 0f;
    public float timeDelay = 0f;
    private bool isStart = false;

    //enemy's target
    private Transform target;

    public bool isWarning = false;
    public List<GameObject> enemyWasCreated;

    public enum SpawnState { Warning, Spawn, None };
    private SpawnState currentState = SpawnState.Warning;

    private void LoadData()
    {
        timeSpawn = scriptEnemy3.timeSpawn;
        timerProcessSpawn = scriptEnemy3.timeProcessSpawn;

        //
        timeDelay = scriptEnemy3.timeDelay;

        minRangeSpawn = scriptEnemy3.minRangeSpawn;
        maxRangeSpawn = scriptEnemy3.maxRangeSpawn;
    }


    private void Start()
    {
        LoadData();
        target = TransformTheBall.GetInstance().GetTransform();
    }

    private void Update()
    {
        if(!isStart && SceneMgr.GetInstance().IsStateInGame())
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
                case SpawnState.Warning:
                {
                    StateSpawnWarning();
                    break;
                }
                case SpawnState.Spawn:
                {    
                    StateSpawn();
                    break;
                }
                case SpawnState.None:
                {

                    break;
                }
            }
        }
    }

    private void StateSpawnWarning()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            obj.GetComponent<Enemy3>().OnSetWarning(false);

            enemyWasCreated.Add(obj);
            timerProcessSpawn = 0;

        }
    }

    private void StateSpawn()
    {
        if(!isWarning)
        {
            foreach (GameObject obj in enemyWasCreated)
            {
                if (obj != null)
                {
                    obj.GetComponent<Enemy3>().OnSetWarning(true);
                }
                   
            }
            isWarning = true;
        }

        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            obj.GetComponent<Enemy3>().OnSetWarning(true);

            enemyWasCreated.Add(obj);
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

    public void Reset()
    {
        foreach (GameObject obj in enemyWasCreated)
        {
            if (obj != null)
                Destroy(obj);
        }

        timerProcessSpawn = 0;
        timerProcessSpawn = scriptEnemy3.timeProcessSpawn;
        currentState = SpawnState.Warning;
        timeProcessDelay = 0;
        isStart = false;
    }
}
