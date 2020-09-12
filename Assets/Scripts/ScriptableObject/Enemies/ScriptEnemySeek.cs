using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySeek", menuName = "Config/EnemySeek")]
public class ScriptEnemySeek : ScriptableObject
{
    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;
    
    [Header("Do we have set up warning for this enemy")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;

    [Header("Number of enemy have warning when spawn")]
    public int numberOfWarning = 3;

    [Header("Move speed of the enemy seek")]
    public float moveSpeed = 7f;

    [Header("Slowdown when turning of the enemy seek")]
    [Range(0.1f , 5f)]public float slowdownTurning = 0.1f;

    [Header("Time to spawn enemy seek")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    
    [Header("Range to create enemy seek")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy seek")]
    public float distanceWarning = 8f;

}
