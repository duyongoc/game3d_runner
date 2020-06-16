using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy1 : MonoBehaviour
{
    [Header("Enemies will be create")]
    public GameObject enemyPrefab;

    [Header("Data to spawn enemy")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;
    private float timerProcess = 0;
    public float timeToSpawn = 3.0f;

    //enemy's target
    private Transform target;

    public bool isWarning = false;
    public List<GameObject> enemyWasCreated;

    public enum SpawState { Warning, Spawn, None };
    private SpawState currentState;

    private void Start()
    {
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
        timerProcess += Time.deltaTime;
        if (timerProcess >= timeToSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            obj.GetComponent<Enemy1>().OnSetWarning(false);

            enemyWasCreated.Add(obj);
            timerProcess = 0;
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
                    obj.GetComponent<Enemy1>().OnSetWarning(true);
                }
                   
            }
            isWarning = true;
        }

        timerProcess += Time.deltaTime;
        if (timerProcess >= timeToSpawn)
        {
            Vector3 vec = GetRandomPoint();
            GameObject obj = Instantiate(enemyPrefab, vec, Quaternion.identity);
            obj.GetComponent<Enemy1>().OnSetWarning(true);

            enemyWasCreated.Add(obj);
            timerProcess = 0;
        }
    }

    public void FinishWarningAlert()
    {
        timerProcess = 0;
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

        timerProcess = 0;
    }
}
