using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    
    public Transform m_target;

    // parameter passing to camera move following
    public float floatNegativeX;
    public float floatPositiveX;
    public float floatNegativeY;
    public float floatPositiveY;

    private void Update()
    {
       

        FollowingPlayer();
    
            
    }

    void FollowingPlayer()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(m_target.position);
        Vector3 delta = m_target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         if(this.transform.position.x <= floatNegativeX)
            transform.position= new Vector3(floatNegativeX, transform.position.y, -10);
        if(this.transform.position.x >= floatPositiveX)
            transform.position = new Vector3(floatPositiveX, transform.position.y, -10);
        if(this.transform.position.y <= floatNegativeY) 
            transform.position = new Vector3(transform.position.x, floatNegativeY, -10);
        if(this.transform.position.y >= floatPositiveY)
            transform.position = new Vector3(transform.position.x, floatPositiveY, -10);
    }


}
