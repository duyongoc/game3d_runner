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

    private Material currentMaterial;
    private float timer = 0;

    public override void StartState()
    {
        base.StartState();

        if(!SoundMgr.GetInstance().IsPlaying(SceneMgr.GetInstance().m_sceneInGame.m_audioPower))
        {
            SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioPower);  
        }

        currentMaterial = this.GetComponent<Renderer>().material;
        transform.position = new Vector3(transform.position.x ,1 , transform.position.z);
        transform.localScale = new Vector3(3,3,3);
        this.gameObject.tag = "Armor";
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
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            Move();
        }
    }

    public override void EndState()
    {
        base.EndState();
        
        this.GetComponent<Renderer>().material = currentMaterial;
        SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioBackground);
        transform.position = new Vector3(transform.position.x , 0, transform.position.z);
        transform.localScale = Vector3.one;
        this.gameObject.tag = "TheBall";
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
    


}
