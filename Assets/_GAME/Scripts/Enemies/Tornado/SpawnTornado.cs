using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTornado : MonoBehaviour
{
    [Header("Data for Tornado")]
    public ScriptTornado scriptTornado;
    
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
    public List<GameObject> enemyWasCreated;

    public enum SpawnState { SpawnInit, Spawn, None };
    private SpawnState currentState = SpawnState.SpawnInit;
    

    private void LoadData()
    {
        timeSpawn = scriptTornado.timeSpawn;
        timerProcessSpawn = scriptTornado.timeProcessSpawn;

        timeDelay = scriptTornado.timeDelay;

        minRangeSpawn = scriptTornado.minRangeSpawn;
        maxRangeSpawn = scriptTornado.maxRangeSpawn;
    }

    #region UNITY
    private void Start()
    {
        LoadData();
        target = MainCharacter.Instance.GetTransform();
    }

    private void Update()
    {
        if(!isStart && GameMgr.Instance.IsGameRunning)
        {
            timeProcessDelay += Time.deltaTime;
            if(timeProcessDelay >= timeDelay)
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
                    SpawnEnemyInit();
                    
                    break;
                case SpawnState.Spawn: 
                    SpawnEnemy();

                    break;
                case SpawnState.None:
                    break;
            }
        }
    }
    #endregion

    #region Function of state
    private void SpawnEnemyInit()
    {
        Camera.main.GetComponent<CameraFollow>().IsFlowCamera = true;
        currentState = SpawnState.Spawn;
    }

    private void SpawnEnemy()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);

            enemyWasCreated.Add(obj);
            timerProcessSpawn = 0;
        }
    }
    #endregion

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

        currentState = SpawnState.SpawnInit;
    }
}
