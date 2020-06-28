using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IConAmmo : MonoBehaviour
{
    
    public GameObject prefabAmmo;
    // List<GameObject> arr;

    private Transform target;
    private int num = 3;


    void Start()
    {
        target = TransformTheBall.GetInstance().GetTransform();
    }

    void OnTriggerEnter(Collider other)
    {

    }

    IEnumerator Shoot()
    {
        while(num >= 1)
        {
            var arrA = SceneMgr.GetInstance().GetComponent<SpawnEnemy1>().enemyWasCreated;
            var arrB = SceneMgr.GetInstance().GetComponent<SpawnEnemy2>().enemyWasCreated;

            // arr.AddRange(arrA);
            // arr.AddRange(arrB);/

            // float
            // for()

            num--;
            yield return new WaitForSeconds(1);
        }
       


    }

}
