using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    public Rigidbody body;
    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        body.AddForce(new Vector3(0.5f,1,0) * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
