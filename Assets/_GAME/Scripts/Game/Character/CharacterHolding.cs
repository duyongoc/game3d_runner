using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolding : StateCharacter
{
    [Header("Move speed")]
    public float moveSpeed = 5f;

    [Header("Timer load Game Over Scene")]
    public float timerLoad = 2f;
    private float timerProcess = 0f;
    private Transform target;


    //
    //= private
    private MainCharacter character;


    public override void StartState()
    {
        base.StartState();

        timerProcess = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);


        //
        timerProcess += Time.deltaTime;
        if(timerProcess >= timerLoad)
        {
            // explosion when player dead
            // Instantiate(character.ballExplosion, transform.position, Quaternion.identity);

            // loading gameover scene;
            // var mgr = SceneMgr.GetInstance();
            // mgr.ChangeState(mgr.m_sceneGameOver);

            //set state none the ball when game over
            character.ChangeState(character.GetCharacterNone);
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
        target = tran;
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }

}
