using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPower : MonoBehaviour
{
    [Header("Spawn the power ")]
    [SerializeField]private GameObject prefabPower = default;
    //[SerializeField]private float timeSpawn = 3f;
    //private float timer = 0f;

    [Header("Transform to create the power")]
    [SerializeField]private Transform[] transArr = default;
    public List<GameObject> listTheHole;
    private bool isCreated = false;

    private void Update()
    {
        if(!isCreated)
        {
            if(SceneMgr.GetInstance().IsStateInGame())
            {
                SpawnThePower();
                isCreated = true;
            }
        }

    }

    private void SpawnThePower()
    {
        for(int i = 0 ; i < transArr.Length; i++ )
        {
            GameObject obj = Instantiate(prefabPower, transArr[i].position, Quaternion.identity);
            
            listTheHole.Add(obj);
        }
    }


}
