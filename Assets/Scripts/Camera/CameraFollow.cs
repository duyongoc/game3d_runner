using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;

    Vector3 velocity = Vector3.zero;
    public float smoothFactor = 0.15f;

    public int offset;

    private void Start()
    {
        //offset = (int)transform.position.z;
    }

    private void FixedUpdate()
    {
        if (m_target == null)
        {
            m_target = TransformTheBall.GetInstance().GetTransform();
        }

        Vector3 newPostion = new Vector3(m_target.position.x, transform.position.y, m_target.position.z);
        newPostion.z += offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothFactor * Time.deltaTime);
        //Debug.Log(newPostion);
    }

    public void ChangeTarget(Transform tar, float smoother)
    {
        smoothFactor = smoother;
        m_target = tar;
    }
}
