using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePower : MonoBehaviour
{
    
    //
    //= inspector
    [SerializeField] private GameObject prefabPlane;


    //
    //= private 
    private MainCharacter character;
    private EBoost currentState = EBoost.None;

    private GameObject thePlane;
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
            case EBoost.Start:
                StartBoost();
                break;

            case EBoost.Process:
                ProcessBoost();
                break;

            case EBoost.End:
                EndBoost();
                break;

            case EBoost.None:
                NoneBoost();
                break;
        }
    }
    #endregion


    private void StartBoost()
    {
        if (!SoundMgr.Instance.IsPlaying(SoundMgr.Instance.SFX_CHARACTER_SHIELD))
            SoundMgr.PlaySound(SoundMgr.Instance.SFX_CHARACTER_SHIELD);

        

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
        // this.duration = duration;
    }

    
    private void CreateThePlane()
    {
        thePlane = Instantiate(prefabPlane, transform);
        
    }
    

    private void CacheComponent()
    {
        character = MainCharacter.Instance;
        duration = character.CONFIG_CHARACTER.timeShield;
    }

}
