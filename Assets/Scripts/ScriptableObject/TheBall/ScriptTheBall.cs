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

    [Header("Paramater Turning")]
    public float speedIncrease = 3f;
    public float timerSpeedUp = 3.5f;
    public float engineForce = 1.7f;
    public float turnSpeed = 170f;


}
