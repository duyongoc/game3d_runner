using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    
    public Obstacle[] listObstacle;

    public bool isStart = false;

    private void Start()
    {
        listObstacle = this.GetComponentsInChildren<Obstacle>();

        foreach(Obstacle ob in listObstacle)
        {
            ob.gameObject.SetActive(false);
        }
    }
    
    private void OnSetup()
    {
        foreach(Obstacle ob in listObstacle)
        {
            float randX = Random.Range(-5f, 5f);
            float randZ = Random.Range(-5f, 5f);

            ob.gameObject.transform.position = new Vector3(
                ob.gameObject.transform.position.x + randX,
                0f, 
                ob.gameObject.transform.position.z + randZ);

            if(!ob.gameObject.activeSelf)
            {
                ob.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(isStart)
        {
            OnSetup();
            isStart = false;
        }
    }

    public void Reset()
    {
        OnSetup();
    }
}
