using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChangeColor : MonoBehaviour
{
    [Header("The Ball")]
    public TheBall theBall;
    public GameObject boomSpecial;
    public GameObject boomEffect;

    [Header("Slider process")]
    public Slider sliderProcess;
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

    private int currentIndex = 0;

    private void Awake()
    {
        sliderProcess.value = 0;
        ChangeDataTheBall(currentIndex);
        
    }

    private void Start()
    {
        totalProcessFinish = timeProcessFinish;
    }

    private void Update()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            if(theBall.CurrentState == theBall.m_ballMove)
            {
                currentTimeProcess += Time.deltaTime;
                sliderProcess.value = (float)currentTimeProcess/ timeProcessFinish;
            }   
            
            if(currentTimeProcess >= totalProcessFinish )
            {
                currentIndex = currentIndex >= 3 ? 0 : currentIndex += 1;
                ChangeDataTheBall(currentIndex);

                // destroy all of enemy with certain radius
                //DestroyEnemyWithCertainRadius(distanceRadius);
                theBall.ChangeState(theBall.m_ballPower);

                totalProcessFinish += timerPlusPerProcessFinish;
                currentTimeProcess = 0;
            }
        }
    }

    private void DestroyEnemyWithCertainRadius(float radius)
    {
        Collider[] nearObjects = Physics.OverlapSphere(theBall.transform.position , radius);

        foreach(Collider obj in nearObjects)
        {
            if(obj.tag != "Enemy3" && obj.tag.Contains("Enemy"))
            {
                var temp = obj.GetComponentInParent<DestroyEnemy>();
                temp.DestroyWithExplosion();
            }
        }

        Instantiate(boomSpecial, theBall.transform.position, Quaternion.Euler(-90, 0 , 0));
        Instantiate(boomEffect, theBall.transform.position, Quaternion.Euler(-90, 0 , 0));
    }

    private void ChangeDataTheBall(int index)
    {
        theBall.ballRenderer.material = materialData.arrayMaterials[index].ballMaterial;
        theBall.shapeRenderer.material = materialData.arrayMaterials[index].directShape;
        theBall.ballExplosion = materialData.arrayMaterials[index].ballExplosion;
        theBall.particleMoving = materialData.arrayMaterials[index].particleMoving;
        sliderImage.color = colors[index];
    }   

    public void Reset()
    {
        totalProcessFinish = timeProcessFinish;
        currentTimeProcess = 0;
        sliderProcess.value = 0;

        //
        currentIndex = 0;
        ChangeDataTheBall(currentIndex);
    }

}
