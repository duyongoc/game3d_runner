using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGravity : StateBall
{
    [Header("Move speed")]
    public float moveSpeed = 5f;

    private Vector3 target;

    public override void StartState()
    {
        base.StartState();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        transform.position = Vector3.MoveTowards(transform.position,target, Time.deltaTime * moveSpeed);
    }

    public override void EndState()
    {
        base.EndState();
        
    }

    public void SetTarget(Transform tran)
    {
        target = tran.position;
    }
}
