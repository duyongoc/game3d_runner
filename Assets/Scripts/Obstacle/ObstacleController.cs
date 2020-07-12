using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Obstacle[] listObstacle;

    private void Start()
    {
        listObstacle = this.GetComponentsInChildren<Obstacle>();
        OnSetup();
    }
    
    private void OnSetup()
    {
        foreach(Obstacle ob in listObstacle)
        {
            int randX = Random.Range(0, 360);
            int randY = Random.Range(0, 360);

            ob.gameObject.transform.rotation = Quaternion.Euler(randX, randY, 0);

            if(!ob.gameObject.activeSelf)
            {
                ob.gameObject.SetActive(true);
            }
        }
    }

    public void Reset()
    {
        OnSetup();
    }

}
