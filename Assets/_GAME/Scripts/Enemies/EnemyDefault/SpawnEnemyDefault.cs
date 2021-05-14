using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyDefault : MonoBehaviour, ISpawnObject
{
    [Header("Active object")]
    public bool isActive = false;

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
    private bool isWarning = false;

    [Header("Game's param change in phase")]
    public float moveSpeed;
    public float timeToSpawn;
    private float timerRemainSpawn = 0f;

    private void LoadData()
    {
        //set up warning from enemy
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;

        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;

        moveSpeed = scriptEnemy.moveSpeed;
        timeToSpawn = scriptEnemy.timeSpawn;
    }

    #region UNITY
    private void Start()
    {
        LoadData();
        target = MainCharacter.Instance.GetTransform();
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            switch (currentState)
            {
                case SpawnState.Init:
                    InitSpawnWarningEnemy();
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

    #region Function of State
    private void InitSpawnWarningEnemy()
    {
        if(!isWarning)
        {
            CreateEnemyWarning(false, new Vector3(0,0,10));
            CreateEnemyWarning(false, new Vector3(10,0,10));
            CreateEnemyWarning(false, new Vector3(-10,0,10));
            currentState = SpawnState.Spawn;
            return;
        }

        CreateEnemyWarning(true, new Vector3(0,0,10));
        CreateEnemyWarning(true, new Vector3(10,0,10));
        CreateEnemyWarning(true, new Vector3(-10,0,10));
        isWarning = false;
        currentState = SpawnState.Spawn;
    }

    private void SpawnEnemy()
    {
        timerRemainSpawn += Time.deltaTime;
        if (timerRemainSpawn >= timeToSpawn)
        {
            GameObject obj = Instantiate(enemyPrefab, GetRandomPoint(), Quaternion.identity);
            obj.GetComponent<EnemyDefault>().MoveSpeed = moveSpeed;

            enemyWasCreated.Add(obj);
            timerRemainSpawn = 0;
        }
    }
    #endregion

    private void CreateEnemyWarning( bool warn, Vector3 vec)
    {
        GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
        obj.GetComponent<EnemyDefault>().SetWarning(warn);
        enemyWasCreated.Add(obj);
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
        enemyWasCreated.Clear();

        timerRemainSpawn = 0f;
        
        //Game's param change in phase
        moveSpeed = 0;
        timeToSpawn = scriptEnemy.timeSpawn;

        currentState = SpawnState.Init;
    }

    public void SetInPhaseObject(bool active, float speed = 0, float spawn = 0)
    {
        this.gameObject.SetActive(active);
        moveSpeed += speed;
        timeToSpawn = spawn;
    }
}
