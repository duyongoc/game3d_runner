using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDetection : MonoBehaviour
{

    //
    //= private
    private Entity mEntity;


    #region UNITY
    private void Start()
    {
        mEntity = this.GetComponentInParent<Entity>();
    }

    private void Update()
    {
        foreach (Entity enemy in mEntity.detected.ToArray())
        {
            if (enemy == null)
            {
                mEntity.detected.Remove(enemy);
            }
            //else if(enemy.health == 0) mEntity.detected.Remove(enemy);
        }
    }
    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if (other != null && (other.tag.Contains("Enemy") || other.tag == "Tornado"))
        {
            mEntity.detected.Add(other.GetComponent<Entity>());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        mEntity.detected.Remove(other.GetComponent<Entity>());
    }
}
