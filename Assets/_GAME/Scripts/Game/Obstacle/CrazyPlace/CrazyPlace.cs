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
    public GameObject placement;


    // [private]
    private bool rotatePlacement = false;
    private Animator m_animator;
    private Collider m_collider;


    #region UNITY
    private void Start()
    {
        m_animator = this.GetComponent<Animator>();
        m_collider = this.GetComponent<Collider>();
    }

    private void Update()
    {
        if (isTrigger)
        {
            Invoke("CrazyTimeFinish", timeTriggerFinish);
            isTrigger = false;
        }

        if (rotatePlacement)
            placement.transform.Rotate(new Vector3(0, -1, 0) * 2);
    }
    #endregion



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isTrigger)
            {
                m_animator.SetBool("Attack", true);
                rotatePlacement = true;
                Invoke("CrazyTimeStart", timeTrigger);
            }
        }
    }


    private void CrazyTimeFinish()
    {
        m_animator.SetBool("Attack", false);
        rotatePlacement = false;
        isTrigger = false;
    }


    private void CrazyTimeStart()
    {
        GameObject tmp = Instantiate(crazyBoom, transform.position, Quaternion.identity);
        SpawnCrazyPlace.s_instance.ListCrazyPlaceCreated.Add(tmp);
        isTrigger = true;
    }

}
