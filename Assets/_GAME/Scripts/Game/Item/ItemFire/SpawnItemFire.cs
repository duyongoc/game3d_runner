using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemFire : MonoBehaviour
{


    [Header("Spawn the power ")]
    [SerializeField]private GameObject prefabIcon = default;

    [Header("Num of power will be create")]
    public int numberPower = 20;
    public float timeSpawn = 3f;
    public List<GameObject> thePowerWasCreated;


    // [private]
    private float timer = 0;
    private bool isCreated = false;


    private void Update()
    {
        if(!isCreated)
        {
            // if(SceneMgr.GetInstance().IsGameRunning)
            {
                SpawnPowerWhenGameStart();
                isCreated = true;
            }
        }
        
        // create power per timeSpawn second
        // if(SceneMgr.GetInstance().IsGameRunning && iSValid())     
        {
            timer += Time.deltaTime;
            if(timer > timeSpawn)
            {
                float posX = Random.Range(-40f, 40f);
                float posZ = Random.Range( -30f, 50f);
                GameObject obj = Instantiate(prefabIcon, new Vector3(posX, 0f, posZ), Quaternion.identity);
                thePowerWasCreated.Add(obj);
                timer = 0;
            }
        }
    }


    private bool iSValid()
    {
        if(thePowerWasCreated.Count < 3)
            return true;

        return false;
    }


    private void SpawnPowerWhenGameStart()
    {
        for(int i = 0 ; i < numberPower; i++ )
        {
            float posX = Random.Range(-40f, 40f);
            float posZ = Random.Range( -30f, 50f);
            GameObject obj = Instantiate(prefabIcon, new Vector3(posX, 0f, posZ), Quaternion.identity);
            thePowerWasCreated.Add(obj);
        }
    }


    public void Reset()
    {
        foreach(GameObject obj in thePowerWasCreated)
        {
            Destroy(obj);
        }

        thePowerWasCreated.Clear();
        isCreated = false;
    }

}
