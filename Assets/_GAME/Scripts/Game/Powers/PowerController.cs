using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EBoost
{
    Start,
    Process,
    End,
    None
}


public class PowerController : Singleton<PowerController>
{
    
    public ShieldPower shieldPower;

    #region UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion

}
