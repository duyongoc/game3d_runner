using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    // [private]
    private GroundController groundController;



    #region UNTIY
    private void Start()
    {
        CacheComponent();
    }

    // private void Update()
    // {
    // }
    #endregion



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            groundController.ArrangeMap(new Vector3(transform.position.x, 0f, transform.position.z));
        }
    }


    private void CacheComponent()
    {
        groundController = GetComponentInParent<GroundController>();
    }

}
