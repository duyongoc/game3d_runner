using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamage
{


    //
    //= private 
    private MainCharacter character;



    #region UNITY
    private void Start()
    {   
        CacheComponent();

    }

    // private void Update()
    // {
    // }
    #endregion


    public void TakeDamage(float damage)
    {
        character.PlayerDead();
        GameMgr.Instance.ChangeState(STATEGAME.GAMEOVER);
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }   

}
