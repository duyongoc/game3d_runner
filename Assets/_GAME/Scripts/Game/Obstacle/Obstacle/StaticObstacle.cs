using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    [Header("Make dissolve effect when destroy obstacle")]
    public Material marDissolve;
    private Material marDefault;
    
    [Header("Renderer obstacle")]
    public GameObject render;

    [Header("Particle ostacle")]
    public GameObject particle;


    #region UNITY
    private void Start()
    {
        marDefault = render.GetComponent<Renderer>().material;
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyDefault" || other.tag == "EnemySeek")
        {
            Instantiate(particle, this.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            var temp = other.GetComponent<IDamage>();
            temp?.TakeDamage(0);
        }
        else if(other.tag == "Player")
        {
            // other.gameObject.GetComponent<MainCharacter>().SetPlayerDead();
            Instantiate(particle, this.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            
            // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "PlayerAbility")
        {
            DissolveObstacle();
            Instantiate(particle, this.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
    }

    public void DissolveObstacle()
    {
        Instantiate(particle, this.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        render.GetComponent<Renderer>().material = marDissolve;
        GetComponent<Collider>().enabled = false;
        StartCoroutine("OnDissolve");
    }

    IEnumerator OnDissolve()
    {
        float timer = 1;
        float process = 0;
        
        while(timer >= 0)
        {
            yield return new WaitForSeconds(0.01f);

            timer -= 0.01f;
            process += 0.01f;
            marDissolve.SetFloat("_processDissolve", process);
        };
        
        marDissolve.SetFloat("_processDissolve", 0);
        render.GetComponent<Renderer>().material = marDefault;
        GetComponent<Collider>().enabled = true;
        gameObject.SetActive(false);
    }
    
}
