using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float m_moveSpeed = 5f;

    private Vector2 vecObject;
    private Vector2 vecOffset;
    private bool isTouch = false;

    [Header("Main Character References")]
    [SerializeField] protected InputJoystick m_joystickAndroid;
    [SerializeField] protected Rigidbody2D m_myBody;

    private Vector3 vectorMovement;

    void Start()
    {
        m_myBody = GetComponent<Rigidbody2D>();
        //m_myAni = GetComponent<Animator>();
    }

    // void Update()
    // {
    //     // if(Input.GetMouseButtonDown(0))
    //     // {
    //     //     m_joystickAndroid.gameObject.transform.position = Input.mousePosition;
    //     // }


    //     Vector2 m_moveDirection =  m_joystickAndroid.inputDirection;

    //     vectorMovement.Set(m_moveDirection.x, m_moveDirection.y, 0);
    //     vectorMovement = vectorMovement.normalized * Time.deltaTime * m_moveSpeed;
    //     m_myBody.MovePosition(transform.position + vectorMovement);

    //     transform.localPosition += vectorMovement;
    // }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            vecObject = Camera.main.ScreenToWorldPoint(transform.position);
            vecOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isTouch = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isTouch = false;
        }

        if(isTouch)
        {
            Vector2 currentPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + vecOffset;
            transform.position = currentPos;
        }
        
    }
}
