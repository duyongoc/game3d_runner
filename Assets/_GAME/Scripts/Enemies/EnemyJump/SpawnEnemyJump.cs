using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyJump : MonoBehaviour, ISpawnObject
{
    [Header("Active object")]
    public bool isActive = false;
    
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

    // create enemy after time
    // private float timeProcessDelay = 0f;
    // private float timeDelay = 0f;
    // private bool isStart = false;

    private bool isWarning = false;
    private int numberOfWarning = 0;

    [Header("Game's param change in phase")]
    public float moveSpeed;
    public float timeToSpawn;
    private float timerProcessSpawn = 0;

    private void LoadData()
    {
        //set up warning from enemy
        isWarning = scriptEnemy.setWarning == SetUp.Warning.Enable ? true : false;
        numberOfWarning = scriptEnemy.numberOfWarning;

        // timeDelay = scriptEnemy.timeDelay;
        minRangeSpawn = scriptEnemy.minRangeSpawn;
        maxRangeSpawn = scriptEnemy.maxRangeSpawn;

        moveSpeed = scriptEnemy.moveSpeed;
        timeToSpawn = scriptEnemy.timeToSpawn;
    }

    #region Singleton
    public static SpawnEnemyJump s_instance;
    private void Awake()
    {
        if(s_instance != null)
            return;

        s_instance = this;
    }
    #endregion

    #region UNITY
    private void Start()
    {
        LoadData();
        target = TransformTheBall.GetInstance().GetTransform();
    }

    private void Update()
    {
        //spawn enemy with delay time
        // if(!isStart)
        // {
        //     timeProcessDelay += Time.deltaTime;
        //     if(timeProcessDelay >= timeDelay)
        //     {
        //         isStart = true;
        //         timeProcessDelay = 0;
        //     }
        // }
        
        if ( SceneMgr.GetInstance().IsStateInGame())
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

    public void SetInPhaseObject(bool active, float speed = 0, float spawn = 0)
    {
        this.gameObject.SetActive(active);
        moveSpeed += speed;
        timeToSpawn = spawn;
    }

    public void AddEnemyWasCreated(GameObject ene)
    {
        enemyWasCreated.Add(ene);
    }

    public static SpawnEnemyJump GetInstance() 
    {
        return s_instance;
    }

    public void Reset()
    {
        foreach (GameObject obj in enemyWasCreated)
        {
            if (obj != null)
                Destroy(obj);
        }
        enemyWasCreated.Clear();

        numberOfWarning = scriptEnemy.numberOfWarning;
        // timeProcessDelay = 0;
        // isStart = false;

        moveSpeed = 0;
        timeToSpawn = scriptEnemy.timeToSpawn;
        timerProcessSpawn = 0;

        currentState = SpawnState.SpawnWarning;
    }

}
