using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{

    // [private] 
    private MainCharacter character;
    private EBoost currentState = EBoost.None;

    private GameObject shieldEffect;
    private float duration;



    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EBoost.Start: StartBoost(); break;
            case EBoost.Process: ProcessBoost(); break;
            case EBoost.End: EndBoost(); break;
            case EBoost.None: NoneBoost(); break;
        }
    }
    #endregion



    private void StartBoost()
    {
        if (!SoundMgr.Instance.IsPlaying(SoundMgr.Instance.SFX_CHARACTER_SHIELD))
            SoundMgr.PlaySound(SoundMgr.Instance.SFX_CHARACTER_SHIELD);

        SetShieldPower(1f, "PlayerAbility", true, false);
        StartCoroutine("ScalingTheShield");

        currentState = EBoost.Process;
    }


    private void ProcessBoost()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
            currentState = EBoost.End;
    }


    private void EndBoost()
    {
        SoundMgr.PlaySound(SoundMgr.Instance.SFX_BACKGROUND);
        SetShieldPower(0f, "Player", false, true);
        character.transform.localScale = Vector3.one;
        currentState = EBoost.None;
    }


    private void NoneBoost()
    {
        // TODO
    }


    public void TriggerAbility(float duration)
    {
        currentState = EBoost.Start;
        this.duration = duration;
    }


    private void SetShieldPower(float y, string tagName, bool visibleShield, bool useGravity)
    {
        shieldEffect.SetActive(visibleShield);
        character.gameObject.tag = tagName;
        character.GetRigidbody.useGravity = useGravity;
        character.transform.position = new Vector3(character.transform.position.x, y, character.transform.position.z);
    }


    private IEnumerator ScalingTheShield()
    {
        float marValue = 0;
        while (transform.localScale.x < 2f)
        {
            character.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 1.25f, 1.25f), 1f * Time.deltaTime);
            marValue = marValue >= 1 ? 1 : (marValue + 0.05f);
            var mar = shieldEffect.GetComponent<Renderer>().material;
            mar.SetFloat("shieldEffectAlpha", marValue);
            yield return new WaitForSeconds(0.05f);
        }
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
        shieldEffect = character.GetShieldEffect;
        duration = character.CONFIG_CHARACTER.timeShield;
    }

}
