using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove2 : StateBall
{
    [Header("Set Speed up")]
    public GameObject speedUpEffect;
    public float speedIncrease = 4f;
    public float timerSpeedUp = 7f;

    public bool isActiveInputMobile = false;

    private Vector3 m_vectorMovement;
    private float timer = 0;

    public float engineForce;

    [SerializeField]
	float speedForce = 10f;
	float torqueForce = 5f;
	float driftFactor = 0.9f;

    [Header("Test")]
    public float turnSpeed = 5;

    public Transform centerPos1;
    public Transform centerPos2;

    public Transform shape;

    //
    public GameObject ParticleTurning;
    public float timeParTurning = 0.2f;
    private float timerTurning = 0f;

    public GameObject trail;
    GameObject temp = null;
    bool isCreate = false;
    
    
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
            Move2();
        }

    }
    
    public override void EndState()
    {
        base.EndState();

    }
    #endregion

    private void Move2()
    {
       
        if(Input.GetMouseButton(0))
        {
            if(!isCreate)
            {
                temp = Instantiate(trail, transform.position, Quaternion.identity);
                temp.transform.parent = transform;
                isCreate = true;
            }
           
            
            float moveTurn = Input.mousePosition.x;
            if(moveTurn < Screen.width / 2 && moveTurn > 0)
            {
                transform.Rotate(-Vector3.up, turnSpeed * Time.deltaTime, Space.World);
                
                Vector3 vec = centerPos1.position - transform.position;
                //vec =  new Vector3(vec.x, 0f, vec.z);
                owner.m_rigidbody.AddForce(vec.normalized * engineForce , ForceMode.Impulse);
                owner.m_rigidbody.AddTorque(vec.normalized * engineForce *2 , ForceMode.Impulse);
                
                shape.localRotation = Quaternion.Lerp(shape.localRotation, Quaternion.Euler(0f, 0f, 100f), Time.deltaTime * 5);
            
            }
            else if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
            {
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.World);
                
                Vector3 vec = centerPos2.position - transform.position ;
                //vec =  new Vector3(vec.x, 0f, vec.z);
                owner.m_rigidbody.AddForce(-vec.normalized  * engineForce , ForceMode.Impulse);
                owner.m_rigidbody.AddTorque(-vec.normalized* engineForce *2, ForceMode.Impulse);

               shape.localRotation = Quaternion.Lerp(shape.localRotation, Quaternion.Euler(0f, 0f, -100f), Time.deltaTime * 5);
            }
            timerTurning += Time.deltaTime;
            if(timerTurning > timeParTurning)
            {
                Instantiate(ParticleTurning, transform.position, Quaternion.identity);
                timerTurning = 0;
            }

        }
        else{
            if(isCreate)
            {
                if(temp != null)
                {
                    temp.transform.parent = null;
                    Destroy(temp, 2f);
                }
                    
                isCreate = false;
            }
            else{
                timer += Time.deltaTime;
                if(timer > owner.timeParMoving )
                {
                    Instantiate(owner.particleMoving, transform.position, Quaternion.identity);
                    timer = 0;
                }
            }
        }

        if(shape.rotation.z != 0f)
            shape.localRotation = Quaternion.Lerp(shape.localRotation,Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5);
        
        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime);

        
        
    }

    private void Move()
    {
        if(Input.GetMouseButton(0))
        {
            float moveTurn = Input.mousePosition.x;
            if(moveTurn < Screen.width / 2 && moveTurn > 0)
            {
                Quaternion target = Quaternion.Euler (0, transform.localRotation.y + (-45f), 0);
			    transform.localRotation = Quaternion.Lerp(transform.localRotation,target,Time.deltaTime * 2);
                owner.m_rigidbody.AddForce(new Vector3(0,0,1) * engineForce , ForceMode.Impulse);
                owner.m_rigidbody.AddTorque(new Vector3(0,0,1) * engineForce , ForceMode.Impulse);
            }
            else if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
            {
                Quaternion target = Quaternion.Euler (0,transform.localRotation.y + 45f, 0);
			    transform.localRotation = Quaternion.Lerp(transform.localRotation,target,Time.deltaTime * 2);
                owner.m_rigidbody.AddForce(new Vector3(0,0,1) * engineForce , ForceMode.Impulse);
                owner.m_rigidbody.AddTorque(new Vector3(0,0,-1) * engineForce , ForceMode.Impulse);
               // owner.m_rigidbody. = new Vector3 (0f, -torqueForce, 0f);

            }
            else 
            {
                Quaternion target = Quaternion.Euler (0, 0, 0);
			    transform.localRotation = Quaternion.Lerp(transform.localRotation,target,Time.deltaTime * 5);

            }
        }
       
        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime); 
        
        // timer += Time.deltaTime;
        // if(timer > owner.timeParMoving)
        // {
        //     Instantiate(owner.particleMoving, transform.position, Quaternion.identity);
        //     timer = 0;
        // }
    }

    Vector3 Forward()
    {
        return Vector3.forward * Vector3.Dot(owner.m_rigidbody.velocity, transform.up);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hole"))
        {
            owner.ChangeState(owner.m_ballGravity);
            owner.m_ballGravity.SetTarget(other.transform);
        }
        else if(other.tag.Contains("Enemy") && this.gameObject.tag != "Armor")
        {
            Instantiate(owner.ballExplosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            // loading gameover scene;
            owner.ChangeState(owner.m_ballNone);
            var mgr = SceneMgr.GetInstance();
            mgr.ChangeState(mgr.m_sceneGameOver);
        }
        else if(other.tag == "IconSpeedUp")
        {
            speedUpEffect.SetActive(true);
            owner.moveSpeed += speedIncrease;

            other.gameObject.GetComponent<IConSpeedUp>().MakeEffect();
            Invoke("ResetSpeed", timerSpeedUp);
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

    public void ResetSpeed()
    {
        speedUpEffect.SetActive(false);
        owner.moveSpeed = owner.scriptTheBall.moveSpeed;
    }

    public void Reset()
    {
        if(temp != null)
            Destroy(temp);
        
        shape.localRotation = Quaternion.Euler(0f, 0f, 0f);
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
