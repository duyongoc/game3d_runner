using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyGlobe : MonoBehaviour
{

    [Header("Data for Enemy Globe")]
    public ScriptEnemyGlobe scriptEnemy;

    [Header("Enemies prefab")]
    public GameObject enemyPrefab;

    [Header("State of enemy")]
    public SpawnState currentState = SpawnState.Warning;
    public enum SpawnState { Warning, Spawn, None };

    [Header("Enemy's target")]
    public Transform target;
    public List<GameObject> enemyWasCreated;


    // [private]
    private float timeProcessDelay = 0f;
    private float timeDelay = 0f;

    private bool isStart = false;
    private bool isWarning = false;
    private int numberOfWarning = 0;

    private float minRangeSpawn;
    private float maxRangeSpawn;
    private float timeSpawn;
    private float timerProcessSpawn;



    private void LoadData()
    {
        //set up warning from enemy
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;
        numberOfWarning = scriptEnemy.numberOfWarning;

        timeSpawn = scriptEnemy.timeSpawn;
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;

        //
        timeDelay = scriptEnemy.timeDelay;
        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;
    }


    private void Start()
    {
        LoadData();
        target = MainCharacter.Instance.GetTransform();
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
                case SpawnState.Warning:
                    SpawnEnemyWarning(); break;

                case SpawnState.Spawn:
                    SpawnEnemy(); break;

                case SpawnState.None:
                    break;
            }
        }
    }


    private void SpawnEnemyWarning()
    {
        if (!isWarning)
        {
            currentState = SpawnState.Spawn;
            return;
        }

        timerProcessSpawn += Time.deltaTime;
        if (numberOfWarning >= 0 && timerProcessSpawn >= timeSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);
            obj.GetComponent<EnemyGlobe>().SetWarning(true);
            enemyWasCreated.Add(obj);

            numberOfWarning--;
            if (numberOfWarning == 0)
            {
                isWarning = false;
            }
            timerProcessSpawn = 0;
        }
    }


    private void SpawnEnemy()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);
            enemyWasCreated.Add(obj);
            timerProcessSpawn = 0;
        }
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
        timerProcessSpawn = scriptEnemy.timeProcessSpawn;
        currentState = SpawnState.Warning;
        timeProcessDelay = 0;
        isStart = false;
    }
}
