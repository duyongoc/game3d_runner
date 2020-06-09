using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPower : MonoBehaviour
{
    [Header("Power to create")]
    public GameObject[] prefabsPower;

    public List<GameObject> listPower;

    private int currentLevel = 1;

    public void Reset()
    {
        foreach(GameObject obj in listPower)
            Destroy(obj.gameObject);
            
        currentLevel = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Power"))
        {
            switch(currentLevel)
            {
                case 1:
                {
                    GameObject obj = Instantiate(prefabsPower[currentLevel - 1], transform.position, Quaternion.identity);
                    obj.transform.SetParent(transform); 
                    listPower.Add(obj);
                    currentLevel++;
                    
                    break;
                }
                case 2:
                {
                    GameObject obj = Instantiate(prefabsPower[currentLevel - 1], transform.position, Quaternion.identity);
                    obj.transform.SetParent(transform); 
                    listPower.Add(obj);
                    currentLevel++;
                    
                    break;
                }
                case 3:
                {
                    GameObject obj = Instantiate(prefabsPower[currentLevel - 1], transform.position, Quaternion.identity);
                    obj.transform.SetParent(transform); 
                    listPower.Add(obj);
                    currentLevel = 1;
                    
                    break;
                }
            }
            Destroy(other.gameObject);
        }
    }

}
