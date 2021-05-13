using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShield : StateCharacter
{
    
    //
    //= private 
    private float timeShield = 15f;
    private float timeShieldFinish = 12f;
    private float timeShieldRemain = 0f;
    private float timeFinishRemain = 0f;

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


    //
    //= private 
    private MainCharacter character;


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    // private void Update()
    // {
    // }
    #endregion


    #region STATE OF CHARACTER
    public override void StartState()
    {
        base.StartState();

        timeShieldRemain = timeShield;
        timeFinishRemain = timeShieldFinish;
        shieldEffect.SetActive(false);

        //
        // character.timeProcessFinish = timeShieldRemain;
        // character.currentTimeProcess = timeShieldRemain;
        // character.sliderProcess.gameObject.SetActive(true);
        // character.sliderProcess.value = 1;

        // if(!SoundMgr.GetInstance().IsPlaying(SceneMgr.GetInstance().m_sceneInGame.m_audioPower))
        //     SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioPower);  

        SetUpBallPower(1f, "PlayerAbility", true, true, false);
        StartCoroutine("ScaleTheBall");
        Invoke("SetMovingPlayerAbility", 2f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        timeShieldRemain -= Time.deltaTime;
        timeFinishRemain -= Time.deltaTime;



        if (timeShieldRemain <= 0)
        {
            character.ChangeState(character.GetCharacterMove);

            Reset();
            timeShieldRemain = timeShield;
            timeFinishRemain = timeShieldFinish;
        }

        if (timeFinishRemain <= 0f)
        {
            StartCoroutine("PowerProcessFinish", 2.4f);
            timeFinishRemain = timeShieldFinish;
        }
    }

    public override void EndState()
    {
        base.EndState();

        StopAllCoroutines();
        // SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioBackground);

        SetUpBallPower(0f, "Player", false, false, true);
        transform.localScale = Vector3.one;


        if (temp != null)
        {
            temp.transform.parent = null;
            Destroy(temp, 2f);
        }

        //
        // character.sliderProcess.gameObject.SetActive(false);
        // character.sliderProcess.value = 1;
        // character.GetCurrentTimeProcess  = 0;
    }
    #endregion


    IEnumerator ScaleTheBall()
    {
        float marValue = 0;
        while (transform.localScale.x < 2f)
        {
            // scale of the MC
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 1.25f, 1.25f), 1f * Time.deltaTime);

            //set up material
            marValue = marValue >= 1 ? 1 : (marValue + 0.05f);
            var mar = shieldEffect.GetComponent<Renderer>().material;
            mar.SetFloat("shieldEffectAlpha", marValue);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PowerProcessFinish(float timer)
    {
        while (timer >= 0)
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
        character.GetRigidbody.useGravity = useGravity;
    }

    //
    private void SetMovingPlayerAbility()
    {
        if (!isFirst)
        {
            character.FirstTriggerPower = true;
            isFirst = true;
        }
    }

    public void Reset()
    {
        isFirst = false;
    }


    private void CacheDefine()
    {
        timeShield = character.CONFIG_CHARACTER.timeShield;
        timeShieldFinish = character.CONFIG_CHARACTER.timeShieldFinish;
    }

    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }

}
