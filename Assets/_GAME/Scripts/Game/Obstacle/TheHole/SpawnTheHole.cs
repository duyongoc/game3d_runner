using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTheHole : Obstacle
{

    [Header("Setting")]
    [SerializeField] private GameObject prefabsTheHole = default;
    [SerializeField] private Transform[] transArr = default;


    // [private]
    private bool isCreated = false;
    private List<GameObject> listHoleCreated;



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
            SpawHole();
            isCreated = true;
        }
    }
    #endregion

    

    private void SpawHole()
    {
        for (int i = 0; i < transArr.Length; i++)
        {
            GameObject obj = Instantiate(prefabsTheHole, transArr[i].position, Quaternion.identity, transform);
            listHoleCreated.Add(obj);
        }
    }


    public void FinishWarningAlert()
    {
        foreach (GameObject obj in listHoleCreated)
        {
            obj.GetComponent<TheHole>()?.OnSetWarning(true);
        }
    }


    public override void Reset()
    {
        foreach (GameObject obj in listHoleCreated)
            Destroy(obj);

        listHoleCreated.Clear();
        isCreated = false;
    }


    private void CacheComponent()
    {
        listHoleCreated = new List<GameObject>();
    }

}
