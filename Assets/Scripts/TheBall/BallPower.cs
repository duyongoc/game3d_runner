using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPower : StateBall
{
    [Header("The time power")]
    public float timerPower = 15f;
    public float timerPowerProcess = 0f;
    public float timerFinish = 12f;
    public float timerFinishProcess = 0f;

    [Header("Particle change")]
    public Material changeMaterial;
    public GameObject powerEffect;
    public GameObject shield;

    private Material currentMaterial;
    private float timer = 0;

    private bool isFirst = false;

    public override void StartState()
    {
        base.StartState();

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

        timerPowerProcess += Time.deltaTime;
        timerFinishProcess += Time.deltaTime;

        if(timerPowerProcess >= timerPower)
        {
            owner.ChangeState(owner.m_ballMove);

            //Reset();
            timerPowerProcess = 0;
            timerFinishProcess = 0;
        }

        if(timerFinishProcess >= timerFinish)
        {
            StartCoroutine("PowerProcessFinish", 2.5f);
            timerFinishProcess = 0;
        }
        //UpdatePlayerMovement();
        if (SceneMgr.GetInstance().IsStateInGame()
            && owner.isFirstTriggerPower)
        {
            Move();
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
    }

    private void Move()
    {
        if(Input.GetMouseButton(0))
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
        }

        transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime); 
        
        timer += Time.deltaTime;
        if(timer > owner.timeParMoving)
        {
            Instantiate(owner.particleMoving, transform.position, Quaternion.identity);
            timer = 0;
        }
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
