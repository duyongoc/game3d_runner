using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorite : MonoBehaviour
{

    [Header("Data for Meteorte")]
    public ScriptMeteorite scriptMeteorite;

    [Header("Spawn the power ")]
    [SerializeField]private GameObject prefabMeteorite = default;
    //[SerializeField]private float timeSpawn = 3f;
    //private float timer = 0f;

    private float minRangeSpawn;
    private float maxRangeSpawn ;
    
    private float timeSpawn ;
    private float timerProcessSpawn;

    //
    public float timeProcessDelay = 0f;
    public float timeDelay = 0f;
    private bool isStart = false;

    private Transform target;
    public List<GameObject> theMeteoriteCreated;

    public enum SpawnState { Spawn, None };
    private SpawnState currentState = SpawnState.Spawn;

     private void LoadData()
    {
        timeSpawn = scriptMeteorite.timeSpawn;
        timerProcessSpawn = scriptMeteorite.timeProcessSpawn;

        timeDelay = scriptMeteorite.timeDelay;

        minRangeSpawn = scriptMeteorite.minRangeSpawn;
        maxRangeSpawn = scriptMeteorite.maxRangeSpawn;
    }

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
            GameObject obj = Instantiate(prefabMeteorite, vec, Quaternion.identity);
            //obj.GetComponent<Enemy4>().OnSetWarning(true);

            theMeteoriteCreated.Add(obj);
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
        if(theMeteoriteCreated.Count < 3)
            return true;
        return false;
    }

    public void Reset()
    {
        foreach(GameObject obj in theMeteoriteCreated)
        {
            Destroy(obj);
        }

        theMeteoriteCreated.Clear();
        timerProcessSpawn = 0;
    }
}
