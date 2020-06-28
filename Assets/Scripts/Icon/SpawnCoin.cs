using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    [Header("Spawn the power ")]
    [SerializeField]private GameObject prefabIcon = default;
    //[SerializeField]private float timeSpawn = 3f;
    //private float timer = 0f;

    [Header("Num of power will be create")]
    public int numberPower = 20;
    public float timeSpawn = 3f;
    private float timer = 0;

    public List<GameObject> thePowerWasCreated;
    private bool isCreated = false;

    private void Update()
    {
        if(!isCreated)
        {
            if(SceneMgr.GetInstance().IsStateInGame())
            {
                SpawnPowerWhenGameStart();
                isCreated = true;
            }
        }
        
        // create power per timeSpawn second
        if(SceneMgr.GetInstance().IsStateInGame())     
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
