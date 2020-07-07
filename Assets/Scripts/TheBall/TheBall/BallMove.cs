using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : StateBall
{
    [Header("Set Speed up")]
    public GameObject speedUpEffect;
    public float speedIncrease = 4f;
    public float timerSpeedUp = 7f;

    public bool isActiveInputMobile = false;

    private Vector3 m_vectorMovement;
    private float timer = 0;
    
    #region STATE OF PLAYER
    public override void StartState()
    {
        base.StartState();
        this.gameObject.SetActive(true);
        speedUpEffect.SetActive(false);
        
        if(!isActiveInputMobile)
        {
            owner.m_inputMobile.gameObject.SetActive(false);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //UpdatePlayerMovement();
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            Move();
        }

    }
    
    public override void EndState()
    {
        base.EndState();

    }
    #endregion

    private void Move()
    {
        if(Input.GetMouseButton(0))
        {
            switch(isActiveInputMobile)
            {
                // moving the ball with joystick
                // case true:
                // {
                //     float moveTurn = owner.m_inputMobile.InputDirection.x;
                //     if(moveTurn < -0.5f  && moveTurn > -1)
                //         transform.Rotate(-Vector3.up, owner.angleSpeed * Time.deltaTime);
                //     if(moveTurn > 0.5f && moveTurn < 1)
                //         transform.Rotate(Vector3.up, owner.angleSpeed * Time.deltaTime);
                //     break;
                // }
                // moving the ball with touch the scene
                case false:
                {
                    float moveTurn = Input.mousePosition.x;
                    if(moveTurn < Screen.width / 2 && moveTurn > 0)
                    {
                        //Quaternion target = Quaternion.Euler (0, 0, 4f);
			            //transform.localRotation = Quaternion.Lerp(transform.localRotation,target,owner.angleSpeed * Time.deltaTime);
                        transform.Rotate(-Vector3.up, owner.angleSpeed * Time.deltaTime, Space.World);
                    }
                    if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
                    {
                        //Quaternion target = Quaternion.Euler (0, 0, -4f);
			            //transform.localRotation = Quaternion.Lerp(transform.localRotation,target,owner.angleSpeed * Time.deltaTime);
                        transform.Rotate(Vector3.up, owner.angleSpeed * Time.deltaTime, Space.World);
                    }
                    break;
                }
            }
        }

        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime); 
        
        timer += Time.deltaTime;
        if(timer > owner.timeParMoving)
        {
            Instantiate(owner.particleMoving, transform.position, Quaternion.identity);
            timer = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if(other.gameObject.CompareTag("Hole"))
        // {
        //     owner.ChangeState(owner.m_ballGravity);
        //     owner.m_ballGravity.SetTarget(other.transform);
        // }
        // else if(other.tag.Contains("Enemy") && this.gameObject.tag != "Armor")
        // {
        //     Instantiate(owner.ballExplosion, transform.position, Quaternion.identity);
        //     gameObject.SetActive(false);

        //     // loading gameover scene;
        //     owner.ChangeState(owner.m_ballNone);
        //     var mgr = SceneMgr.GetInstance();
        //     mgr.ChangeState(mgr.m_sceneGameOver);
        // }
        // else if(other.tag == "IconSpeedUp")
        // {
        //     speedUpEffect.SetActive(true);
        //     owner.moveSpeed += speedIncrease;

        //     other.gameObject.GetComponent<IConSpeedUp>().MakeEffect();
        //     Invoke("ResetSpeed", timerSpeedUp);
        // }
    }

    void OnCollisionEnter(Collision other)
    {
        // if(other.gameObject.tag == "Ground")
        // {
        //     // loading gameover scene;
        //     var mgr = SceneMgr.GetInstance();
        //     mgr.ChangeState(mgr.m_sceneGameOver);


        //     Instantiate(owner.ballExplosion, transform.position, Quaternion.identity);
        //     gameObject.SetActive(false);
        // }
    }

    public void ResetSpeed()
    {
        speedUpEffect.SetActive(false);
        owner.moveSpeed = owner.scriptTheBall.moveSpeed;
    }

    // private void UpdatePlayerMovement()
    // {
    //     float m_moveHorizontal = owner.m_inputMobile.InputDirection.x;
    //     float m_moveVertical = owner.m_inputMobile.InputDirection.y;
    //     m_vectorMovement.Set(m_moveHorizontal, 0, m_moveVertical);
    //     m_vectorMovement = m_vectorMovement.normalized * Time.deltaTime * 5;

    //     owner.m_rigidbody.MovePosition(transform.position + m_vectorMovement);
    // }



}
