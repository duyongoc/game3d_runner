using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrazyPlace : Obstacle
{

    #region Init
    public static SpawnCrazyPlace s_instance;
    private void Awake()
    {
        if (s_instance != null)
            return;
        s_instance = this;
    }
    #endregion


    [Header("[Setting]")]
    [SerializeField] private GameObject prefabsCrazyPlace = default;
    [SerializeField] private int size = 3;
    [SerializeField] private Transform[] transArr = default;


    // [private] 
    private bool isCreated = false;
    private List<int> listIndex;
    private List<GameObject> listCrazyPlaceCreated;


    // [properties]
    public List<GameObject> ListCrazyPlaceCreated { get => listCrazyPlaceCreated; set => listCrazyPlaceCreated = value; }



    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        if (isCreated)
            return;

        if (GameMgr.Instance.IsGameRunning)
        {
            SpawCrazyPlace();
            isCreated = true;
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

            } while (repeat);

            GameObject obj = Instantiate(prefabsCrazyPlace, transArr[rand].position, Quaternion.identity, transform);
            listIndex.Add(rand);
            listCrazyPlaceCreated.Add(obj);
        }
    }


    public override void Reset()
    {
        foreach (GameObject obj in listCrazyPlaceCreated)
        {
            Destroy(obj);
        }

        isCreated = false;
        listIndex.Clear();
        listCrazyPlaceCreated.Clear();
    }


    private void CacheComponent()
    {
        listCrazyPlaceCreated = new List<GameObject>();
        listIndex = new List<int>();
    }

}
