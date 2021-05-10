using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flycam : MonoBehaviour
{

    [Header("Create random point")] 
    public float moveSpeed;
    public float distanceRadius;
    private Vector3 pointRandom;

    //the ball
    private MainCharacter mainCharacter;

    private enum ObjectState { Moving, None }
    private ObjectState currentState = ObjectState.Moving;


    private void Start()
    {
        mainCharacter = MainCharacter.Instance.GetComponent<MainCharacter>();
        pointRandom = GetRandomPoint();
    }

    void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            switch (currentState)
            {
                case ObjectState.Moving:
                {
                    // if(mainCharacter.CurrentState == mainCharacter.m_ballPower
                    //     && !mainCharacter.isFirstTriggerPower)
                    // {
                    //     return;
                    // }

                    FlycamMoving();
                    break;
                }
                case ObjectState.None:
                {

                    break;
                }
            }
        }
        
    }

    private void FlycamMoving()
    {
        float distance = Vector3.Distance(transform.position, pointRandom);
        if(distance <= 0.1f)
        {
            pointRandom = GetRandomPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, pointRandom, moveSpeed * Time.deltaTime);

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
        
        return new Vector3(hit.position.x, transform.position.y, hit.position.z);
    }
}
