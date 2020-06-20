using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy3", menuName = "Config/Enemy3")]
public class ScriptEnemy3 : ScriptableObject
{
    [Header("Move speed of the enemy 3")]
    public float moveSpeed = 3f;

    [Header("Time to spawn enemy 3")]
    public float timeSpawn = 3f;
    public float timeProcessSpawn = 2.5f;

    [Header("Range to create enemy 3")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy 3")]
    public float distanceWarning = 5f;


}
