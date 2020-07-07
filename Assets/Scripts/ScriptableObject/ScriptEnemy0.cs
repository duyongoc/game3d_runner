using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy0", menuName = "Config/Enemy0")]
public class ScriptEnemy0 : ScriptableObject
{
    [Header("Move speed of the enemy 0")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy 1")]
    public float timeSpawn = 3f;
    public float timeProcessSpawn = 2.5f;

    [Header("Range to create enemy 1")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy 1")]
    public float distanceWarning = 8f;
}
