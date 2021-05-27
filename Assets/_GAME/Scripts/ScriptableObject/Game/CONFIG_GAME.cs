using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CONFIG_GAME", menuName = "CONFIG/CONFIG_GAME")]
public class CONFIG_GAME : ScriptableObject
{
    [Header("_CAMERA")]
    public float moveSpeed = 4f; 
    public float smoothFactor = 10f;
    public float timeZoomCamera = 60f; // depend on ms
    [Space(5)]
    public float posOriginX = 0f; 
    public float posOriginY = 15f; 
    public float posOriginZ = -10f; 

    [Header("_other")]
    public float tmp = 0f; 

}
