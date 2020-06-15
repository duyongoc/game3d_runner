using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : StateBall
{

    private Vector3 m_vectorMovement;
    public bool isActiveInputMobile = false;

    private float timer = 0;
    

    #region STATE OF PLAYER
    public override void StartState()
    {
        base.StartState();
        this.gameObject.SetActive(true);
        
        if(!isActiveInputMobile)
        {
            owner.m_inputMobile.gameObject.SetActive(false);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //Player moving
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
        if(other.gameObject.CompareTag("Hole"))
        {
            owner.ChangeState(owner.m_ballGravity);
            owner.m_ballGravity.SetTarget(other.transform);
        }
        else if(other.tag.Contains("Enemy"))
        {
            // loading gameover scene;
            var mgr = SceneMgr.GetInstance();
            mgr.ChangeState(mgr.m_sceneGameOver);


            Instantiate(owner.ballExplosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            // loading gameover scene;
            var mgr = SceneMgr.GetInstance();
            mgr.ChangeState(mgr.m_sceneGameOver);


            Instantiate(owner.ballExplosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
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
