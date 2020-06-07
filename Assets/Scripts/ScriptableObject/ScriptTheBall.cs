using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TheBall", menuName = "Config/TheBall")]
public class ScriptTheBall : ScriptableObject
{
    [Header("Move speed of the ball")]
    public float moveSpeed = 5f;

    [Header("Angle speed of the ball")]
    public float angleSpeed  = 100;

    [Header("Active mobile input")]
    public bool isActiveMobileInput = false;

}
