using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPower : StateBall
{
    [Header("The time power")]
    public float timerPower = 15f;
    public float timerFinish = 12f;
    public float timerPowerProcess = 0f;
    public float timerFinishProcess = 0f;

    [Header("Particle change")]
    public Material changeMaterial;
    public GameObject powerEffect;
    public GameObject shield;

    private Material currentMaterial;
    // private float timer = 0;

    private bool isFirst = false;

    //
    [Header("Test")]
    public float engineForce;
    public float turnSpeed = 5;

    public Transform centerPos1;
    public Transform centerPos2;

    public Transform shape;

    public GameObject ParticleTurning;
    public float timeParTurning = 0.2f;
    private float timerTurning = 0f;

    public GameObject trail;
    GameObject temp = null;
    bool isCreate = false;
    private float timerMoving = 0;



    public override void StartState()
    {
        base.StartState();

        timerPowerProcess = timerPower;
        timerFinishProcess = timerFinish;

        //
        owner.timeProcessFinish = timerPowerProcess;
        owner.currentTimeProcess = timerPowerProcess;
        owner.sliderProcess.gameObject.SetActive(true);
        owner.sliderProcess.value = 1;

        currentMaterial = this.GetComponent<Renderer>().material;
        if(!SoundMgr.GetInstance().IsPlaying(SceneMgr.GetInstance().m_sceneInGame.m_audioPower))
            SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioPower);  

        SetUpBallPower(1f, "Armor", true, true, false);
        StartCoroutine("ScaleTheBall");
        Invoke("SetMovingBallPower", 3f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        timerPowerProcess -= Time.deltaTime;
        timerFinishProcess -= Time.deltaTime;

        //load slider 
        owner.currentTimeProcess -= Time.deltaTime;
        owner.sliderProcess.value = (float)owner.currentTimeProcess/ timerFinish;

        if(timerPowerProcess <= 0)
        {
            owner.ChangeState(owner.m_ballMove);

            //Reset();
            timerPowerProcess = timerPower;
            timerFinishProcess = timerFinish;
        }

        if(timerFinishProcess <= 0f)
        {
            StartCoroutine("PowerProcessFinish", 2.4f);
            timerFinishProcess = timerFinish;
        }
        //UpdatePlayerMovement();
        if (SceneMgr.GetInstance().IsStateInGame()
            && owner.isFirstTriggerPower)
        {
            Move2();
        }
        
    }

    public override void EndState()
    {
        base.EndState();

        StopAllCoroutines();
        SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioBackground);

        SetUpBallPower(0f,"TheBall", false, false, true);
        transform.localScale = Vector3.one;

        this.GetComponent<Renderer>().material = currentMaterial;
        
        if(temp != null)
        {
            temp.transform.parent = null;
            Destroy(temp, 2f);
        }

        //
        owner.sliderProcess.gameObject.SetActive(false);
        owner.sliderProcess.value = 1;
        owner.currentTimeProcess = 0;
    }

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
                timerMoving += Time.deltaTime;
                if(timerMoving > owner.timeParMoving )
                {
                    int rand = Random.Range(0, 180);
                    Instantiate(owner.particleMoving, transform.position,Quaternion.Euler(0,0,rand));
                    timerMoving = 0;
                }
            }
        }

        if(shape.rotation.z != 0f)
            shape.localRotation = Quaternion.Lerp(shape.localRotation,Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5);
        
        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime);

    }

    IEnumerator ScaleTheBall()
    {
        while(transform.localScale.x < 3f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(3,3,3) , 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PowerProcessFinish(float timer)
    {
        while(timer >= 0)
        {
            yield return new WaitForSeconds(0.25f);
            if(this.GetComponent<Renderer>().material == currentMaterial)
                this.GetComponent<Renderer>().material = changeMaterial;
            else
                this.GetComponent<Renderer>().material = currentMaterial;

            timer -= 0.25f;
        }
       
    }

    //
    private void SetUpBallPower(float y, string tagName, bool visibleEffect, bool visibleShield, bool useGravity)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        this.gameObject.tag = tagName;
        powerEffect.SetActive(visibleEffect);
        shield.SetActive(visibleShield);
        owner.m_rigidbody.useGravity = useGravity;
    }

    //
    private void SetMovingBallPower()
    {
        if(!isFirst)
        {
            owner.isFirstTriggerPower = true;
            isFirst = true;
        }
    }

    public void Reset()
    {
        isFirst = false;
    }

}
