using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoftObstacle : MonoBehaviour
{

    [Header("Setting")]
    public SoftObstacle[] listObstacle;
    public bool isStart = false;



    #region UNITY
    private void Start()
    {
        listObstacle = this.GetComponentsInChildren<SoftObstacle>();
        foreach (SoftObstacle ob in listObstacle)
        {
            ob.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isStart)
        {
            OnSetup();
            isStart = false;
        }
    }
    #endregion



    private void OnSetup()
    {
        foreach (SoftObstacle ob in listObstacle)
        {
            // just not appy for sorf obstacle
            // float randX = Random.Range(-5f, 5f);
            // float randZ = Random.Range(-5f, 5f);

            // ob.gameObject.transform.position = new Vector3(
            //     ob.gameObject.transform.position.x + randX,
            //     0f, 
            //     ob.gameObject.transform.position.z + randZ);

            if (!ob.gameObject.activeSelf)
            {
                ob.gameObject.SetActive(true);
            }
        }
    }


    public void Reset()
    {
        OnSetup();
    }

}
