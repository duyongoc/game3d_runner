using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : StateCharacter
{
    [Header("The time power")]
    public float timerPower = 15f;
    public float timerFinish = 12f;
    public float timerPowerProcess = 0f;
    public float timerFinishProcess = 0f;

    [Header("Particle change")]
    public GameObject shieldEffect;

    private bool isFirst = false;

    //
    [Header("Test")]
    public Transform shape;

    public Transform centerPos1;
    public Transform centerPos2;

    public GameObject ParticleTurning;
    public float timeParTurning = 0.2f;
    private float timerTurning = 0f;

    public GameObject trail;
    GameObject temp = null;
    private float timer = 0;
    bool isCreate = false;


    public override void StartState()
    {
        base.StartState();

        timerPowerProcess = timerPower;
        timerFinishProcess = timerFinish;
        shieldEffect.SetActive(false);

        //
        owner.timeProcessFinish = timerPowerProcess;
        owner.currentTimeProcess = timerPowerProcess;
        owner.sliderProcess.gameObject.SetActive(true);
        owner.sliderProcess.value = 1;

        if(!SoundMgr.GetInstance().IsPlaying(SceneMgr.GetInstance().m_sceneInGame.m_audioPower))
            SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioPower);  

        SetUpBallPower(1f, "PlayerAbility", true, true, false);
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
            owner.ChangeState(owner.m_characterMove);

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
        if (SceneMgr.GetInstance().IsStateInGame() && owner.isFirstTriggerPower)
        {
            Moving();
        }
        
    }

    public override void EndState()
    {
        base.EndState();

        StopAllCoroutines();
        SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioBackground);

        SetUpBallPower(0f,"Player", false, false, true);
        transform.localScale = Vector3.one;

        
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

    private void Moving()
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
                transform.Rotate(-Vector3.up, owner.turnSpeed * Time.deltaTime, Space.World);
                
                Vector3 vec = centerPos1.position - transform.position;
                //vec =  new Vector3(vec.x, 0f, vec.z);
                owner.m_rigidbody.AddForce(vec.normalized * owner.engineForce , ForceMode.Impulse);
                owner.m_rigidbody.AddTorque(vec.normalized * owner.engineForce *2 , ForceMode.Impulse);
                
                shape.localRotation = Quaternion.Lerp(shape.localRotation, Quaternion.Euler(0f, 0f, 100f), Time.deltaTime * 5);
            
            }
            else if(moveTurn > Screen.width / 2 && moveTurn < Screen.width)
            {
                transform.Rotate(Vector3.up, owner.turnSpeed * Time.deltaTime, Space.World);
                
                Vector3 vec = centerPos2.position - transform.position ;
                //vec =  new Vector3(vec.x, 0f, vec.z);
                owner.m_rigidbody.AddForce(-vec.normalized  * owner.engineForce , ForceMode.Impulse);
                owner.m_rigidbody.AddTorque(-vec.normalized* owner.engineForce *2, ForceMode.Impulse);

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
                    int rand = Random.Range(0, 180);
                    Instantiate(owner.particleMoving, transform.position,Quaternion.Euler(0,0,rand));
                    timer = 0;
                }
            }
        }

        if(shape.rotation.z != 0f)
            shape.localRotation = Quaternion.Lerp(shape.localRotation,Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5);
        
        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime);

    }

    IEnumerator ScaleTheBall()
    {
        float marValue = 0;
        while(transform.localScale.x < 2f)
        {
            // scale of the MC
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 1.25f, 1.25f) , 1f * Time.deltaTime);
            
            //set up material
            marValue = marValue >= 1 ? 1 : (marValue + 0.05f);
            var mar = shieldEffect.GetComponent<Renderer>().material;
            mar.SetFloat("shieldEffectAlpha", marValue);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PowerProcessFinish(float timer)
    {
        while(timer >= 0)
        {
            yield return new WaitForSeconds(0.25f);

            timer -= 0.25f;
        }
       
    }

    //
    private void SetUpBallPower(float y, string tagName, bool visibleEffect, bool visibleShield, bool useGravity)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        this.gameObject.tag = tagName;
        shieldEffect.SetActive(visibleShield);
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
