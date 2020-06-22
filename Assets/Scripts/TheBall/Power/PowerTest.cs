using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTest : MonoBehaviour
{
    [Header("Power to create")]
    public GameObject[] prefabsPower;

    [Header("The time power")]
    public float timerPower = 15f;
    public float timerPowerProcess = 0f;
    public float timerFinish = 12f;
    public float timerFinishProcess = 0f;

    private TheBall theBall;

    [Header("Sound plus power")]
    public AudioClip m_audioPower;

    //
    //public List<GameObject> listPower;
    //public int currentLevel = 0;

    public bool isTriggerPower = false;

    private void Start()
    {
        theBall = GetComponentInParent<TheBall>();
    }

    private void Update()
    {
        if(isTriggerPower)
        {
            GameObject obj2 = Instantiate(prefabsPower[2], transform.position, Quaternion.identity);
            obj2.transform.SetParent(transform); 
            
            if(!SoundMgr.GetInstance().IsPlaying(SceneMgr.GetInstance().m_sceneInGame.m_audioPower))
            {
                SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioPower);  
            }
                

            timerPowerProcess += Time.deltaTime;
            timerFinishProcess += Time.deltaTime;

            if(timerFinishProcess >= timerFinish)
            {
                GameObject obj = Instantiate(prefabsPower[3], transform.position, Quaternion.identity);
                obj.transform.SetParent(transform); 

                //prefabsPower[2].SetActive(false);
                //listPower.Add(obj);

                timerFinishProcess = 0;
            }

            if(timerPowerProcess >= timerPower)
            {
                SoundMgr.GetInstance().PlaySound(SceneMgr.GetInstance().m_sceneInGame.m_audioBackground);
                Reset();

                timerPowerProcess = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(currentLevel == 3)
        //     return;

        // //Debug.Log(other.name);
        // if(other.gameObject.tag == "Power" )
        // {
        //     switch(currentLevel)
        //     {
        //         case 0:
        //         {
        //             GameObject obj = Instantiate(prefabsPower[currentLevel], transform.position, Quaternion.identity);
        //             obj.transform.SetParent(transform); 
        //             listPower.Add(obj);
        //             currentLevel++;
                    
        //             break;
        //         }
        //         case 1:
        //         {
        //             GameObject obj = Instantiate(prefabsPower[currentLevel], transform.position, Quaternion.identity);
        //             obj.transform.SetParent(transform); 
        //             listPower.Add(obj);
        //             currentLevel++;
                    
        //             break;
        //         }
        //         case 2:
        //         {
        //             GameObject obj = Instantiate(prefabsPower[currentLevel], transform.position, Quaternion.identity);
        //             obj.transform.SetParent(transform); 
        //             listPower.Add(obj);
        //             currentLevel++;
                    
        //             break;
        //         }
        //     }

        //     SoundMgr.GetInstance().PlaySoundOneShot(m_audioPower);
        //     Destroy(other.gameObject);
        // }
    }

    public void Reset()
    {
        // foreach(GameObject obj in listPower)
        //     Destroy(obj.gameObject);
        // listPower.Clear();
            
        isTriggerPower = false;
        timerPowerProcess = 0;
        timerFinishProcess = 0;
    }
}
