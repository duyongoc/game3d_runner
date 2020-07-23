using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    public Rigidbody body;
    public Vector2 vectorDir = new Vector2(0.5f, 1);

    public GameObject effect; 

    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        body.AddForce(vectorDir * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plane")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

}
