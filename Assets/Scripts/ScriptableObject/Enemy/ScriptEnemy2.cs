using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy2", menuName = "Config/Enemy2")]
public class ScriptEnemy2 : ScriptableObject
{
    [Header("Move speed of the Enemy 2")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy 2")]
    public float timeProcessSpawn = 0f;
    public float timeSpawn = 4f;

    [Header("Range to create enemy 2")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy 2")]
    public float distanceWarning = 8f;



}
