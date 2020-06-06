using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : StateBall
{
    private Vector3 m_vectorMovement;

    //
    [Header("Material's weapon player")]
    public Renderer m_gunMaticle;

    public float m_speed = 5f;
    public float angleSpeed = 5f;
    public bool isActiveInputMobile = true;

    

    #region STATE OF PLAYER
    public override void StartState()
    {
        base.StartState();
        
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

        Move();
        
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
                        transform.Rotate(-Vector3.up, angleSpeed * Time.deltaTime);

                    if(moveTurn > 0.5f && moveTurn < 1)
                        transform.Rotate(Vector3.up, angleSpeed * Time.deltaTime);

                    break;
                }
                // moving the ball with touch the scene
                case false:
                {
                    float moveTurn = Input.mousePosition.x;

                    if(moveTurn < Screen.width / 2 && moveTurn > 0)
                        transform.Rotate(-Vector3.up, angleSpeed * Time.deltaTime);

                    if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
                        transform.Rotate(Vector3.up, angleSpeed * Time.deltaTime);

                    break;
                }
            }
        }

        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);       
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
