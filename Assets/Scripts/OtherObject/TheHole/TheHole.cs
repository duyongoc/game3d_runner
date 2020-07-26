using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheHole : MonoBehaviour
{
    [Header("Load data")]
    public ScriptTheHole scriptTheHole;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public float distanceWarning = 8f;
    public bool isWarning = false;
    private Transform target;

    // create random point
    private float moveSpeed;
    private float distanceRadius;
    private Vector3 pointRandom;

    //the ball
    private TheBall theBall;

    private enum HoleState { Moving, None }
    private HoleState currentState = HoleState.Moving;

    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }

    void Start()
    {
        warningIcon.SetActive(false);
        moveSpeed = scriptTheHole.moveSpeed;
        distanceRadius = scriptTheHole.distanceRadius;

        target = TransformTheBall.GetInstance().GetTransform();
        theBall = TransformTheBall.GetInstance().GetComponent<TheBall>();
        pointRandom = GetRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            switch (currentState)
            {
                case HoleState.Moving:
                {
                    if(theBall.CurrentState == theBall.m_ballPower
                        && !theBall.isFirstTriggerPower)
                    {
                        return;
                    }

                    HoleMoving();
                    break;
                }
                case HoleState.None:
                {

                    break;
                }
            }
        }
        
    }

    private void HoleMoving()
    {
        if(!isWarning)
        {
            if(Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
            {
                warningIcon.SetActive(true);
                //SceneMgr.GetInstance().GetComponentInChildren<SpawnTheHole>().FinishWarningAlert();
                // Camera.main.GetComponent<CameraFollow>().ChangeTarget(transform, 100);
                // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_scenePauseGame);
                
                StartCoroutine("FinishWarningTheHole");
                isWarning = true;
            }
        }


        float distance = Vector3.Distance(transform.position, pointRandom);
        if(distance <= 0.1f)
        {
            pointRandom = GetRandomPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, pointRandom, moveSpeed * Time.deltaTime);

    }

    IEnumerator FinishWarningTheHole()
    {
        yield return new WaitForSeconds(2f);

        warningIcon.SetActive(false);
        // Camera.main.GetComponent<CameraFollow>().ChangeTarget(target, 10);
        // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneInGame);
    }


    private Vector3 GetRandomPoint()
    {
        NavMeshHit hit;
        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * distanceRadius;
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, distanceRadius, 1);
        }
        while(hit.position.x == Mathf.Infinity && hit.position.y == Mathf.Infinity);
        
        return hit.position;
    }
}
