using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTheHole : MonoBehaviour
{

    [Header("Spawn the hole")]
    [SerializeField]private GameObject prefabsTheHole = default;
    
    [Header("Transform to create the hole")]
    [SerializeField]private Transform[] transArr = default;

    public List<GameObject> listTheHoleWasCreated;

    private bool isCreated = false;

    
    private void Start()
    {
        
    }

    private void Update()
    {
        if(!isCreated)
        {
            if(SceneMgr.GetInstance().IsStateInGame())
            {
                SpawHole();
                isCreated = true;
            }
        }

    }

    private void SpawHole()
    {
        for(int i = 0 ; i < transArr.Length; i++ )
        {
            GameObject obj = Instantiate(prefabsTheHole, transArr[i].position, Quaternion.identity);
            listTheHoleWasCreated.Add(obj);
        }
    }

    public void Reset()
    {
        foreach(GameObject obj in listTheHoleWasCreated)
        {
            Destroy(obj);
        }
        listTheHoleWasCreated.Clear();

        isCreated = false;
    }

}
