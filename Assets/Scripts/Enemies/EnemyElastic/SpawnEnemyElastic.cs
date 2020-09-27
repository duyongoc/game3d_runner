using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyElastic : MonoBehaviour
{
    [Header("Data for Elastic")]
    public ScriptEnemyElastic scriptEnemy;
    
    [Header("Enemies prefab")]
    public GameObject enemyPrefab;

    [Header("State of Enemy")]
    public SpawnState currentState = SpawnState.Spawn;
    public enum SpawnState { Spawn, None };
    
    [Header("Enemy's target")]
    public Transform target;

    public List<GameObject> enemyWasCreated;

    private float minRangeSpawn;
    private float maxRangeSpawn ;
    
    private float timeSpawn ;
    private float timerProcessSpawn;

    private float timeProcessDelay = 0f;
    private float timeDelay = 0f;
    private bool isStart = false;

    private void LoadData()
    {

        timeSpawn = scriptEnemy.timeSpawn;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;

        timeDelay = scriptEnemy.timeDelay;

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

    private void StateSpawn()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            //obj.GetComponent<Enemy4>().OnSetWarning(true);

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
        timeProcessDelay = 0;
        isStart = false;
    }
}
