using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBall : MonoBehaviour
{
    [SerializeField] private InputJoystick m_inputJoystick = default;
    [SerializeField] private float m_offset = -90f;
    [SerializeField] private Vector3 m_direction = Vector3.zero;

    void Update()
    {
        m_direction = m_inputJoystick.inputDirection; 
        if(m_direction.magnitude != 0)
        {
            Vector3 difference = m_direction - transform.localPosition;
            difference = difference.normalized;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ + m_offset);
        }
    }
}
