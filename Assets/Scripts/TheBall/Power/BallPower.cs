using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPower : MonoBehaviour
{
    [Header("Power to create")]
    public GameObject[] prefabsPower;

    [Header("The time power")]
    public float timerPower = 15f;
    public float timer = 0;
    private TheBall theBall;

    [Header("Sound plus power")]
    public AudioClip m_audioPower;

    //
    public List<GameObject> listPower;
    private int currentLevel = 0;

    private void Start()
    {
        theBall = GetComponentInParent<TheBall>();
    }

    private void Update()
    {
        if(currentLevel == 3)
        {
            
            if(!SoundMgr.GetInstance().IsPlaying(SceneMgr.GetInstance().m_sceneInGame.m_audioPower))
            {
                SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioPower);  
            }
                

            timer += Time.deltaTime;
            if(timer >= timerPower)
            {
                SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioBackground);

                Reset();
                timer = 0;
            }
        }

    }


    public void Reset()
    {
        foreach(GameObject obj in listPower)
            Destroy(obj.gameObject);
            
        currentLevel = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Power"))
        {
            switch(currentLevel)
            {
                case 0:
                {
                    GameObject obj = Instantiate(prefabsPower[currentLevel], transform.position, Quaternion.identity);
                    obj.transform.SetParent(transform); 
                    listPower.Add(obj);
                    currentLevel++;
                    
                    break;
                }
                case 1:
                {
                    GameObject obj = Instantiate(prefabsPower[currentLevel], transform.position, Quaternion.identity);
                    obj.transform.SetParent(transform); 
                    listPower.Add(obj);
                    currentLevel++;
                    
                    break;
                }
                case 2:
                {
                    GameObject obj = Instantiate(prefabsPower[currentLevel], transform.position, Quaternion.identity);
                    obj.transform.SetParent(transform); 
                    listPower.Add(obj);
                    currentLevel++;
                    
                    break;
                }
            }

            SoundMgr.GetInstance().PlaySoundOneShot(m_audioPower);
            Destroy(other.gameObject);
        }
    }

}
