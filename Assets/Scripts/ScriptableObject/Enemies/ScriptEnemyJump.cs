using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyJump", menuName = "Config/EnemyJump")]
public class ScriptEnemyJump : ScriptableObject
{

    [Header("Do we have set up warning for this enemy")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;

    [Header("Number of enemy have warning when spawn")]
    public int numberOfWarning = 3;

    [Header("Jump speed enemy jump", order = 2)]
    public float jumpSpeed = 1f;

    [Header("Jump high enemy jump")]
    public float jumpHigh = 3.5f;

    [Header("Move speed of the enemy jump")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy jump")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 10f;

    [Header("Range to create enemy jump")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy jump")]
    public float distanceWarning = 10f;
}
