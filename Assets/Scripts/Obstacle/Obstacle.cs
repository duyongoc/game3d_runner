﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(particle, other.transform.position, Quaternion.identity);
        if(other.tag == "TheBall")
        {
            other.gameObject.SetActive(false);
            SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponentInParent<Enemy1>();
            if(temp)
                Destroy(temp.gameObject);
            Destroy(other.gameObject);
        }
    }
}