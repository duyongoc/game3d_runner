using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGravity : StateBall
{
    [Header("Move speed")]
    public float moveSpeed = 5f;

    [Header("Timer load Game Over Scene")]
    public float timerLoad = 3f;
    private float timerProcess = 0f;

    //create explosion when player dead
    private bool isExplosion;

    private Vector3 target;

    public override void StartState()
    {
        base.StartState();

        timerProcess = 0;
        isExplosion = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        transform.position = Vector3.MoveTowards(transform.position,target, Time.deltaTime * moveSpeed);

        // explosion when player dead
        if(!isExplosion)
        {
            var distance = Vector3.Distance(transform.position, target);
            if(distance <= 0.1f)
            {
                Instantiate(owner.ballExplosion, transform.position, Quaternion.identity);
                isExplosion = true;
            }
        }

        //
        timerProcess += Time.deltaTime;
        if(timerProcess >= timerLoad)
        {
            // loading gameover scene;
            var mgr = SceneMgr.GetInstance();
            mgr.ChangeState(mgr.m_sceneGameOver);

            //set state none the ball when game over
            owner.ChangeState(owner.m_ballNone);
            this.gameObject.SetActive(false);
            timerProcess = 0f;
        }

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
