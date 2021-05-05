using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGameMgr : MonoBehaviour
{

    #region Singleton
    public static SettingGameMgr s_instance;
    private void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;
    }

    public static SettingGameMgr GetInstance()
    {
        return s_instance;
    }
    #endregion

    

}
