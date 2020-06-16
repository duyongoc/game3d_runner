using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTheHole : MonoBehaviour
{

    [Header("Spawn the hole")]
    [SerializeField]private GameObject prefabsTheHole = default;
    
    [Header("Transform to create the hole")]
    [SerializeField]private Transform[] transArr = default;

    //
    private bool isCreated = false;
    public List<GameObject> holeWasCreated;

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
            holeWasCreated.Add(obj);
        }
    }

    public void FinishWarningAlert()
    {
        foreach (GameObject obj in holeWasCreated)
        {
            if (obj != null)
            {
                obj.GetComponent<TheHole>().OnSetWarning(true);
            }
                
        }
    }

    public void Reset()
    {
        foreach(GameObject obj in holeWasCreated)
        {
            Destroy(obj);
        }
        holeWasCreated.Clear();

        isCreated = false;
    }

}
