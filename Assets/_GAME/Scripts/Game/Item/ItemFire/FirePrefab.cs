using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePrefab : MonoBehaviour
{

    [Header("Effect of Fire")]
    public GameObject effect;
    public float moveSpeed = 400;
    public Vector3 vectorDir = new Vector3(0.5f, 1, 0);


    // [private]
    private Rigidbody body;


    #region UNITY
    private void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody>();
        body.AddForce(vectorDir * moveSpeed);

        float rotX = Random.Range(0, 360);
        float rotZ = Random.Range(0, 360);
        transform.localRotation = Quaternion.Euler(rotX, transform.rotation.y, rotZ);
    }

    // private void Update()
    // {
    // }
    #endregion



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ThePlane")
        {
            Instantiate(effect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(-90, 0, 0));
            Destroy(this.gameObject);
        }

    }
}
