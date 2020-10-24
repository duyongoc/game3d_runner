using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrazyPlace : MonoBehaviour
{
    [Header("Spawn Crazy Place")]
    [SerializeField] private GameObject prefabsCrazyPlace = default;

    [Header("Transform to create Crazy Place")]
    [SerializeField] private Transform[] transArr = default;

    public int size = 3;

    //
    private bool isCreated = false;
    public List<GameObject> crazyPlaceWasCreated;


    //
    // private 
    //
    public List<int> listIndex = default;


    #region Init
    public static SpawnCrazyPlace s_instance;
    private void Awake()
    {
        if (s_instance != null)
            return;
        s_instance = this;
    }
    #endregion

    #region UNITY
    private void Update()
    {
        if (!isCreated)
        {
            if (SceneMgr.GetInstance().IsStateInGame())
            {
                SpawCrazyPlace();
                isCreated = true;
            }
        }
    }
    #endregion

    private void SpawCrazyPlace()
    {
        int rand;
        for (int i = 0; i < size; i++)
        {
            var repeat = false;
            do
            {
                rand = Random.Range(0, transArr.Length);
                repeat = false;

                foreach (int ind in listIndex)
                    if (ind == rand)
                        repeat = true;

                //Debug.Log(repeat);            
            } while (repeat);

            GameObject obj = Instantiate(prefabsCrazyPlace, transArr[rand].position, Quaternion.identity);
            listIndex.Add(rand);
            crazyPlaceWasCreated.Add(obj);
        }
    }

    public void Reset()
    {
        foreach (GameObject obj in crazyPlaceWasCreated)
        {
            Destroy(obj);
        }
        crazyPlaceWasCreated.Clear();
        listIndex.Clear();

        isCreated = false;
    }
}
