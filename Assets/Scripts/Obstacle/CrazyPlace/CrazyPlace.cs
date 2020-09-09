using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyPlace : MonoBehaviour
{
    [Header("Trigger crazy time")]
    public float timeTrigger = 2f;
    public float timeTriggerFinish = 5f;
    public bool isTrigger = false;

    [Header("Crazy Boom")]
    public GameObject crazyBoom;

    //
    private Animator m_animator;
    private Collider m_collider;

    private void Start()
    {
        m_animator = this.GetComponent<Animator>();
        m_collider = this.GetComponent<Collider>();
    }

    private void Update()
    {
        if(isTrigger)
        {
            Invoke("CrazyTimeFinish", timeTriggerFinish);
            isTrigger = false;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {   
            if(!isTrigger)
            {
                m_animator.SetBool("Attack", true);
                Invoke("CrazyTimeStart", timeTrigger);
            }
        }
    }

    private void CrazyTimeFinish()
    {
        m_animator.SetBool("Attack", false);
        isTrigger = false;
    }

    private void CrazyTimeStart()
    {
        GameObject tmp = Instantiate(crazyBoom, transform.position, Quaternion.identity);
        SpawnCrazyPlace.s_instance.crazyPlaceWasCreated.Add(tmp);
        
        isTrigger = true;
    }

}
