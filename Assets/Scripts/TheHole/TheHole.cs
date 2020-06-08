using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheHole : MonoBehaviour
{
    public ScriptTheHole scriptTheHole;

    private float moveSpeed;
    private float distanceRadius;
    private Vector3 target;

    void Start()
    {
        moveSpeed = scriptTheHole.moveSpeed;
        distanceRadius = scriptTheHole.distanceRadius;

        target = GetRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target);
        if(distance <= 0.1f)
        {
            target = GetRandomPoint();
        }


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
