using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChangeColor : MonoBehaviour
{
    [Header("The Ball")]
    public MainCharacter mainCharacter;
    public GameObject boomSpecial;
    public GameObject boomEffect;

    [Header("Slider process")]
    // public Slider sliderProcess;
    public Image sliderImage;
    public float timeProcessFinish = 10f;
    public float currentTimeProcess = 0;

    public float timerPlusPerProcessFinish = 10f;
    private float totalProcessFinish;

    [Header("Color of Slider")]
    public Color[] colors;
    
    [Header("2D Array Marterial")]
    public MaterialData materialData;

    [Header("Radius distance to destroy enemy when the ball change color")]
    public float distanceRadius = 10f;
    private int currentIndex = 2;

    private void Awake()
    {
        //sliderProcess.value = 0;
        ChangeDataTheBall(currentIndex);
        
    }

    private void Start()
    {
        totalProcessFinish = timeProcessFinish;
    }

    private void Updateee()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            // if(theBall.CurrentState == theBall.m_ballMove)
            // {
            //     if(!sliderProcess.gameObject.activeSelf)
            //         sliderProcess.gameObject.SetActive(true);
                    
            //     currentTimeProcess += Time.deltaTime;
            //     sliderProcess.value = (float)currentTimeProcess/ totalProcessFinish;
            // }   
            
            if(currentTimeProcess >= totalProcessFinish )
            {
                currentIndex = currentIndex >= 3 ? 0 : currentIndex += 1;
                ChangeDataTheBall(currentIndex);

                // destroy all of enemy with certain radius
                //DestroyEnemyWithCertainRadius(distanceRadius);
                //sliderProcess.gameObject.SetActive(false);
                // mainCharacter.ChangeState(mainCharacter.m_ballPower);

                totalProcessFinish += timerPlusPerProcessFinish;
                currentTimeProcess = 0;
            }
        }
    }

    private void DestroyEnemyWithCertainRadius(float radius)
    {
        Collider[] nearObjects = Physics.OverlapSphere(mainCharacter.transform.position , radius);

        foreach(Collider obj in nearObjects)
        {
            if(obj.tag != "Enemy3" && obj.tag.Contains("Enemy"))
            {
                //var temp = obj.GetComponentInParent<DestroyEnemy>();
                //temp.DestroyWithExplosion();
            }
        }

        Instantiate(boomSpecial, mainCharacter.transform.position, Quaternion.Euler(-90, 0 , 0));
        Instantiate(boomEffect, mainCharacter.transform.position, Quaternion.Euler(-90, 0 , 0));
    }

    private void ChangeDataTheBall(int index)
    {
        // mainCharacter.ballRenderer.material = materialData.arrayMaterials[index].ballMaterial;
        // mainCharacter.shapeRenderer.material = materialData.arrayMaterials[index].directShape;
        // mainCharacter.ballExplosion = materialData.arrayMaterials[index].ballExplosion;
        // mainCharacter.particleMoving = materialData.arrayMaterials[index].particleMoving;
        // sliderImage.color = colors[index];
    }   

    public void Reset()
    {
        totalProcessFinish = timeProcessFinish;
        currentTimeProcess = 0;
        //sliderProcess.value = 0;

        //
        currentIndex = 2;
        ChangeDataTheBall(currentIndex);
    }

}
