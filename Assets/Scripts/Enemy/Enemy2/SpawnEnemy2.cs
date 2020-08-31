using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy2 : MonoBehaviour
{
    [Header("Data for Enemy 2")]
    public ScriptEnemy2 scriptEnemy2;
    
    [Header("Enemies will be create")]
    public GameObject enemyPrefab;

    [Header("Data to spawn enemy")]
    private float minRangeSpawn;
    private float maxRangeSpawn ;
    
    private float timeSpawn ;
    private float timerProcessSpawn ;

    //enemy's target
    private Transform target;

    public bool isWarning = false;
    public List<GameObject> enemyWasCreated;

    public enum SpawState { Warning, Spawn, None };
    private SpawState currentState;

    private void LoadData()
    {
        timeSpawn = scriptEnemy2.timeSpawn;
        timerProcessSpawn = scriptEnemy2.timeProcessSpawn;

        minRangeSpawn = scriptEnemy2.minRangeSpawn;
        maxRangeSpawn = scriptEnemy2.maxRangeSpawn;
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
                case SpawState.Warning:
                {
                    StateSpawnWarning();
                    break;
                }
                case SpawState.Spawn:
                {    
                    StateSpawn();
                    break;
                }
                case SpawState.None:
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
            obj.GetComponent<Enemy2>().OnSetWarning(false);

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
                    obj.GetComponent<Enemy2>().OnSetWarning(true);
                }
                   
            }
            isWarning = true;
        }

        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            obj.GetComponent<Enemy2>().OnSetWarning(true);

            enemyWasCreated.Add(obj);
            timerProcessSpawn = 0;
        }
    }

    public void FinishWarningAlert()
    {
        timerProcessSpawn = 0;
        currentState = SpawState.Spawn;
    }

    private void SpawEnemy()
    {
        
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

        timerProcessSpawn = 0;
    }

}
