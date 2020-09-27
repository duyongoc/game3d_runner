using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrazyPlace : MonoBehaviour
{
    [Header("Spawn Crazy Place")]
    [SerializeField]private GameObject prefabsCrazyPlace = default;
    
    [Header("Transform to create Crazy Place")]
    [SerializeField]private Transform[] transArr = default;

    //
    private bool isCreated = false;
    public List<GameObject> crazyPlaceWasCreated;

    #region Init
    public static SpawnCrazyPlace s_instance;
    private void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;
    }
    #endregion

    #region UNITY
    private void Update()
    {
        if(!isCreated)
        {
            if(SceneMgr.GetInstance().IsStateInGame())
            {
                SpawCrazyPlace();
                isCreated = true;
            }
        }
    }
    #endregion

    private void SpawCrazyPlace()
    {
        for(int i = 0 ; i < transArr.Length; i++ )
        {
            GameObject obj = Instantiate(prefabsCrazyPlace, transArr[i].position, Quaternion.identity);
            crazyPlaceWasCreated.Add(obj);
        }
    }

    public void Reset()
    {
        foreach(GameObject obj in crazyPlaceWasCreated)
        {
            Destroy(obj);
        }
        crazyPlaceWasCreated.Clear();

        isCreated = false;
    }
}
