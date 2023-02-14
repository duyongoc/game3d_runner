using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{


    [Header("Make dissolve effect when destroy obstacle")]
    public Material marDissolve;
    public GameObject render;
    public GameObject particle;


    // [private]
    private Material marDefault;



    #region UNITY
    private void Start()
    {
        marDefault = render.GetComponent<Renderer>().material;
    }

    // private void Update()
    // {
    // }
    #endregion



    public void DissolveObstacle()
    {
        Instantiate(particle, this.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        render.GetComponent<Renderer>().material = marDissolve;
        GetComponent<Collider>().enabled = false;
        StartCoroutine("OnDissolve");
    }


    private IEnumerator OnDissolve()
    {
        float timer = 1;
        float process = 0;

        while (timer >= 0)
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


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyDefault":
            case "EnemySeek":
                particle.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "Player":
                particle.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                other.GetComponent<IDamage>()?.TakeDamage(0);
                break;

            case "PlayerAbility":
                DissolveObstacle();
                particle.SpawnToGarbage(transform.localPosition, Quaternion.identity);
                break;
        }
    }

}
