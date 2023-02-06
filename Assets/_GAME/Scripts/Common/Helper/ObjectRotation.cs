using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{

    [Header("[Setting]")]
    public float speed = 5f;
    public Vector3 vector;


    #region UNITY
    private void Start()
    {
        int rand = Random.Range(0, 2);
        speed = rand == 1 ? speed * -1 : speed;
    }

    private void FixedUpdate()
    {
        transform.Rotate(vector * speed);
    }
    #endregion

}
