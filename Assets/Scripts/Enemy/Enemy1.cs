using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D m_rigidbody2D;

    public float speed = 5f;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        Vector2 vecForce = Vector2.zero - (Vector2)transform.position;
        m_rigidbody2D.AddForce(vecForce * speed);
    }
    
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * speed);
    
    }
}
