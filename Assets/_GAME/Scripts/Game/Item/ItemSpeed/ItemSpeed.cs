using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : MonoBehaviour
{

    public GameObject iconEffect;

    public void MakeEffect()
    {
        Instantiate(iconEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}
