using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChangeColor : MonoBehaviour
{
    [Header("The Ball")]
    public TheBall theBall;

    [Header("Slider process")]
    public Slider sliderProcess;
    public Image sliderImage;
    public float timerPlusPerProcessFinish = 10f;
    public float timeProcessFinish = 10f;
    public float currentTimeProcess = 0;

    [Header("Color of Slider")]
    public Color[] colors;
    
    [Header("2D Array Marterial")]
    public MaterialData materialData;

    private int currentIndex = 0;

    private void Awake()
    {
        sliderProcess.value = 0;
        ChangeDataTheBall(currentIndex);
        
    }

    private void Update()
    {
        if (SceneMgr.GetInstance().IsStateInGame())
        {
            currentTimeProcess += Time.deltaTime;
            sliderProcess.value = (float)currentTimeProcess/ timeProcessFinish;
            
            if(currentTimeProcess >= timeProcessFinish )
            {
                currentIndex = currentIndex >= 3 ? 0 : currentIndex += 1;
                ChangeDataTheBall(currentIndex);

                timeProcessFinish += timerPlusPerProcessFinish;
                currentTimeProcess = 0;
            }
        }
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
        timeProcessFinish = timerPlusPerProcessFinish;
        currentTimeProcess = 0;
        sliderProcess.value = 0;

        //
        currentIndex = 0;
        ChangeDataTheBall(currentIndex);
    }

}
