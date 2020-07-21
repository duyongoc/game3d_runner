using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy0 : MonoBehaviour
{
    [Header("Load data Enemy 0")]
    public ScriptEnemy0 scriptEnemy0;

    [Header("Enemies will be create")]
    public GameObject enemyPrefab;

    private float minRangeSpawn;
    private float maxRangeSpawn;
    
    private float timeToSpawn;
    private float timerProcessSpawn;

    //enemy's target
    private Transform target;
    public bool isWarning = false;
    public List<GameObject> enemyWasCreated;

    public enum SpawnState { First, Warning, Spawn, None };
    private SpawnState currentState;

    private void LoadData()
    {
        timeToSpawn = scriptEnemy0.timeSpawn;
        timerProcessSpawn = scriptEnemy0.timeProcessSpawn;

        minRangeSpawn = scriptEnemy0.minRangeSpawn;
        maxRangeSpawn = scriptEnemy0.maxRangeSpawn;
    }

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
                                obj.GetComponent<Enemy0>().OnSetWarning(true);
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
            obj.GetComponent<Enemy0>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj);

            GameObject obj2 = Instantiate(enemyPrefab, new Vector3(20,0,10), Quaternion.identity);
            obj2.GetComponent<Enemy0>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj2);

            GameObject obj3 = Instantiate(enemyPrefab, new Vector3(-20,0,10), Quaternion.identity);
            obj3.GetComponent<Enemy0>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj3);
        }
        else
        {
            GameObject obj = Instantiate(enemyPrefab, new Vector3(0,0,40), Quaternion.identity);
            obj.GetComponent<Enemy0>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj);

            GameObject obj2 = Instantiate(enemyPrefab, new Vector3(20,0,10), Quaternion.identity);
            obj2.GetComponent<Enemy0>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj2);

            GameObject obj3 = Instantiate(enemyPrefab, new Vector3(-20,0,10), Quaternion.identity);
            obj3.GetComponent<Enemy0>().OnSetWarning(isWarning);
            enemyWasCreated.Add(obj3);
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
            obj.GetComponent<Enemy0>().OnSetWarning(warning);

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
        currentState = SpawnState.First;
    }
}
