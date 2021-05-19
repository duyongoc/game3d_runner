using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorite : Obstacle
{

    //
    //= inspector
    [SerializeField] private ScriptMeteorite scriptMeteorite;
    [SerializeField] private GameObject prefabMeteorite = default;
    public enum SpawnState { Spawn, None };
    private SpawnState currentState = SpawnState.Spawn;

    //
    //= private
    private float timeDelay = 0f;
    private float timeProcessDelay = 0f;
    private bool isStart = false;

    private float minRangeSpawn;
    private float maxRangeSpawn;

    private float timeSpawn;
    private float timerProcessSpawn;

    private List<GameObject> listMeteoriteCreated;
    private Transform target;


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
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
                case SpawnState.Spawn:
                    StateSpawn();
                    break;

                case SpawnState.None:
                    break;
            }
        }
    }
    #endregion


    private void StateSpawn()
    {
        timerProcessSpawn += Time.deltaTime;
        if (timerProcessSpawn >= timeSpawn)
        {
            GameObject obj = Instantiate(prefabMeteorite,  GetRandomPoint(), Quaternion.identity, transform);
            //obj.GetComponent<Enemy4>().OnSetWarning(true);

            listMeteoriteCreated.Add(obj);
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
        Vector3 vec = new Vector3(hit.position.x, 0.5f, hit.position.z);
        return vec;
    }

    private bool iSValid()
    {
        if (listMeteoriteCreated.Count < 3)
            return true;
        return false;
    }

    public override void Reset()
    {
        foreach (GameObject obj in listMeteoriteCreated)
        {
            Destroy(obj);
        }

        listMeteoriteCreated.Clear();
        timerProcessSpawn = 0;
    }

    private void CacheDefine()
    {
        timeSpawn = scriptMeteorite.timeSpawn;
        timerProcessSpawn = scriptMeteorite.timeProcessSpawn;

        timeDelay = scriptMeteorite.timeDelay;

        minRangeSpawn = scriptMeteorite.minRangeSpawn;
        maxRangeSpawn = scriptMeteorite.maxRangeSpawn;
    }

    private void CacheComponent()
    {
        target = MainCharacter.Instance.GetTransform();
        listMeteoriteCreated = new List<GameObject>();
    }

}
