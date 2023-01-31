using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheHole : MonoBehaviour
{

    private enum HoleState
    {
        Moving,
        None
    }


    [Header("Load data")]
    public ScriptTheHole scriptTheHole;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public float distanceWarning = 8f;
    public bool isWarning = false;

    // [private]
    private Transform target;
    private float moveSpeed;
    private float distanceRadius;
    private Vector3 pointRandom;
    private MainCharacter mainCharacter;
    private HoleState currentState = HoleState.Moving;



    #region UNITY
    private void Start()
    {
        warningIcon.SetActive(false);
        moveSpeed = scriptTheHole.moveSpeed;
        distanceRadius = scriptTheHole.distanceRadius;

        target = MainCharacter.Instance.GetTransform();
        mainCharacter = MainCharacter.Instance.GetComponent<MainCharacter>();
        pointRandom = GetRandomPoint();
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            switch (currentState)
            {
                case HoleState.Moving:
                    // if(mainCharacter.CurrentState == mainCharacter.m_ballPower
                    //     && !mainCharacter.isFirstTriggerPower)
                    // {
                    //     return;
                    // }
                    HoleMoving();
                    break;

                case HoleState.None:
                    break;
            }
        }
    }
    #endregion


    public void OnSetWarning(bool warning)
    {
        isWarning = warning;
    }


    private void HoleMoving()
    {
        if (!isWarning)
        {
            if (Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
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
        if (distance <= 0.1f)
        {
            pointRandom = GetRandomPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, pointRandom, moveSpeed * Time.deltaTime);
    }


    private IEnumerator FinishWarningTheHole()
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
        while (hit.position.x == Mathf.Infinity && hit.position.y == Mathf.Infinity);
        return hit.position;
    }
}
