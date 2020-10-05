using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyElastic", menuName = "CONFIG/Enemy/EnemyElastic")]
public class ScriptEnemyElastic : ScriptableObject
{
    [Header("Do we have set up warning for this enemy")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;

    [Header("Number of enemy have warning when spawn")]
    public int numberOfWarning = 3;

    [Header("Move speed of the Enemy Elastic")]
    public float moveSpeed = 5f;

    [Header("Time to spawn Enemy Elastic")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    
    [Header("Range to create Enemy Elastic")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;

}
