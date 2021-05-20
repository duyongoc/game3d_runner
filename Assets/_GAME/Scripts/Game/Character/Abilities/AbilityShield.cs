using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityShield : MonoBehaviour
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


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }

}
