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
            m_animator.SetBool("Attack", false);
            StartCoroutine("CrazyTimeFinish", timeTriggerFinish);
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

    IEnumerator CrazyTimeFinish(float time)
    {
        yield return new WaitForSeconds(time);
        isTrigger = false;
    }

    private void CrazyTimeStart()
    {
        Instantiate(crazyBoom, transform.position, Quaternion.identity);
        isTrigger = true;
    }

    IEnumerator CrazyTimeStart(float time)
    {
        yield return new WaitForSeconds(time);

        
    }

}
