using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : StateBall
{
    private Vector3 m_vectorMovement;

    //
    [Header("Material's weapon player")]
    public Renderer m_gunMaticle;

    

    #region STATE OF PLAYER
    public override void StartState()
    {
        base.StartState();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        //Player moving
        this.UpdatePlayerMovement();

    }

    public override void EndState()
    {
        base.EndState();

    }
    #endregion

    private void UpdatePlayerMovement()
    {
        float m_moveHorizontal = owner.m_inputMobile.InputDirection.x;
        float m_moveVertical = owner.m_inputMobile.InputDirection.y;
        m_vectorMovement.Set(m_moveHorizontal, 0, m_moveVertical);
        m_vectorMovement = m_vectorMovement.normalized * Time.deltaTime * 5;

        owner.m_rigidbody.MovePosition(transform.position + m_vectorMovement);
    }


}
