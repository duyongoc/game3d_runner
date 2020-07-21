using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy5", menuName = "Config/Enemy5")]
public class ScriptEnemy5 : ScriptableObject
{
    [Header("Move speed of the enemy 5")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy 5")]
    public float timeSpawn = 3f;
    public float timeProcessSpawn = 2.5f;

    [Header("Range to create enemy 5")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;
}
