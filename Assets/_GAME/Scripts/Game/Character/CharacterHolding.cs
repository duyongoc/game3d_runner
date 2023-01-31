using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolding : StateCharacter
{

    [Header("[Setting]")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timerLoad = 2f;


    // [private]
    private Transform target;
    private MainCharacter character;
    private GameObject charExplosion;
    private float timerProcess = 0f;



    #region UNTIY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    public override void StartState()
    {
        base.StartState();
        timerProcess = 0;
    }


    public override void UpdateState()
    {
        base.UpdateState();
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        timerProcess += Time.deltaTime;

        if (timerProcess >= timerLoad)
        {
            charExplosion.SpawnToGarbage(transform.position, Quaternion.identity);
            character.PlayerDead();
            timerProcess = 0f;
        }
    }


    public override void EndState()
    {
        base.EndState();
    }
    

    public void SetTarget(Transform tran)
    {
        target = tran;
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
        charExplosion = character.CONFIG_CHARACTER.prefabExplosion;
    }

}
