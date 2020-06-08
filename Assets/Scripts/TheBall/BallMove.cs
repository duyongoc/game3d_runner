using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : StateBall
{
    
    [Header("Material's the ball when turn around")]
    public GameObject marTurnAround;

    private Vector3 m_vectorMovement;
    public bool isActiveInputMobile = false;

    

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
                case true:
                {
                    float moveTurn = owner.m_inputMobile.InputDirection.x;

                    if(moveTurn < -0.5f  && moveTurn > -1)
                        transform.Rotate(-Vector3.up, owner.angleSpeed * Time.deltaTime);

                    if(moveTurn > 0.5f && moveTurn < 1)
                        transform.Rotate(Vector3.up, owner.angleSpeed * Time.deltaTime);

                    break;
                }
                // moving the ball with touch the scene
                case false:
                {
                    float moveTurn = Input.mousePosition.x;

                    if(moveTurn < Screen.width / 2 && moveTurn > 0)
                        transform.Rotate(-Vector3.up, owner.angleSpeed * Time.deltaTime);

                    if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
                        transform.Rotate(Vector3.up, owner.angleSpeed * Time.deltaTime);

                    break;
                }
            }
        }

        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime); 
 
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
        else if(other.tag.Contains("Ground"))
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
