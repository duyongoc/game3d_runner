using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShield : StateCharacter
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

    GameObject temp = null;


    //
    //= private 
    private MainCharacter character;
    private Vector3 vectorMovement;

    private GameObject prefabMovingParticle;
    private GameObject prefabMovingTrail;

    private float timeParticleMove = 0.2f;
    private float timeParticleRemain = 0f;



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


    public override void StartState()
    {
        base.StartState();

        timerPowerProcess = timerPower;
        timerFinishProcess = timerFinish;
        shieldEffect.SetActive(false);

       if (!SoundMgr.Instance.IsPlaying(SoundMgr.Instance.SFX_CHARACTER_SHIELD))
            SoundMgr.PlaySound(SoundMgr.Instance.SFX_CHARACTER_SHIELD);

        SetUpBallPower(1f, "PlayerAbility", true, true, false);
        StartCoroutine("ScaleTheBall");
        Invoke("SetMovingPlayerAbility", 2f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!GameMgr.Instance.IsGameRunning)
            return;

        timerPowerProcess -= Time.deltaTime;
        timerFinishProcess -= Time.deltaTime;

        if (timerPowerProcess <= 0)
        {
            character.ChangeState(character.GetCharacterMove);
            Reset();

            timerPowerProcess = timerPower;
            timerFinishProcess = timerFinish;
        }

        if (timerFinishProcess <= 0f)
        {
            StartCoroutine("PowerProcessFinish", 2.4f);
            timerFinishProcess = timerFinish;
        }

        UpdateVirtualMovement();

    }

    public override void EndState()
    {
        base.EndState();

        StopAllCoroutines();
        SoundMgr.PlaySound(SoundMgr.Instance.SFX_BACKGROUND);

        SetUpBallPower(0f, "Player", false, false, true);
        transform.localScale = Vector3.one;


        if (temp != null)
        {
            temp.transform.parent = null;
            Destroy(temp, 2f);
        }
    }

    private void UpdateVirtualMovement()
    {
        vectorMovement.Set(character.VirtualMovement.GetDirection.x, 0, character.VirtualMovement.GetDirection.y);
        vectorMovement = vectorMovement.normalized * Time.deltaTime * character.GetMoveSpeed;
        float rotationY = Mathf.Atan2(vectorMovement.x, vectorMovement.z) * Mathf.Rad2Deg;

        CreateMoveTrail();
        UpdateAnimation(vectorMovement.magnitude);

        if (vectorMovement.magnitude == 0) return;

        character.transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        character.GetRigidbody.MovePosition(transform.position + vectorMovement);
    }

    private void CreateMoveTrail()
    {
        timeParticleRemain += Time.deltaTime;
        if (timeParticleRemain > timeParticleMove)
        {
            prefabMovingParticle.SpawnToGarbage(transform.position, Quaternion.identity);
            timeParticleRemain = 0;
        }
    }

    private void UpdateAnimation(float valueMovement)
    {
        if (valueMovement > 0)
            character.SetAnimationMoving();
        else
            character.SetAnimationIdle();
    }

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
        prefabMovingParticle = character.CONFIG_CHARACTER.prefabMovingParticle;
        prefabMovingTrail = character.CONFIG_CHARACTER.prefabMovingTrail;
        timeParticleMove = character.CONFIG_CHARACTER.timeParticleMove;
    }

    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }

}
