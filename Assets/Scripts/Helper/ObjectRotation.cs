using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 vector;

    void Update()
    {
        transform.Rotate( vector * speed);
    }
}
