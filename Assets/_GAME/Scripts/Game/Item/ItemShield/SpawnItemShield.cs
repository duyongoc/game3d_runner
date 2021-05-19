using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemShield : Item
{
    
    //
    //= inspector
    [SerializeField]private GameObject prefabIcon = default;
    [SerializeField]private int numberPower = 20;
    [SerializeField]private float timeSpawn = 3f;


    //
    //= private 
    private List<GameObject> listPowerCreated;
    private bool isCreated = false;
    private float timer = 0;


    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        if(!isCreated)
        {
            if(GameMgr.Instance.IsGameRunning)
            {
                SpawnPowerWhenGameStart();
                isCreated = true;
            }
        }
        
        // create power per timeSpawn second
        if(GameMgr.Instance.IsGameRunning && iSValid() )     
        {
            timer += Time.deltaTime;
            if(timer > timeSpawn)
            {
                float posX = Random.Range(-40f, 40f);
                float posZ = Random.Range( -30f, 50f);
                GameObject obj = Instantiate(prefabIcon, new Vector3(posX, 0.5f, posZ), Quaternion.identity, transform);
                listPowerCreated.Add(obj);

                timer = 0;
            }
        }
    }
    #endregion


    private void SpawnPowerWhenGameStart()
    {
        for(int i = 0 ; i < numberPower; i++ )
        {
            float posX = Random.Range(-40f, 40f);
            float posZ = Random.Range( -30f, 50f);
            GameObject obj = Instantiate(prefabIcon, new Vector3(posX, 0.5f, posZ), Quaternion.identity, transform);
            listPowerCreated.Add(obj);
        }
    }

    private bool iSValid()
    {
        if(listPowerCreated.Count < 3)
            return true;
        return false;
    }

    public override void Reset()
    {
        foreach(GameObject obj in listPowerCreated)
        {
            Destroy(obj);
        }

        listPowerCreated.Clear();
        isCreated = false;
    }

    private void CacheComponent()
    {
        listPowerCreated = new List<GameObject>();
    }

}
