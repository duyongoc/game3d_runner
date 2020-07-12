using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy4", menuName = "Config/Enemy4")]
public class ScriptEnemy4 : ScriptableObject
{
    [Header("Move speed of the enemy 4")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy 4")]
    public float timeSpawn = 3f;
    public float timeProcessSpawn = 2.5f;

    [Header("Range to create enemy 4")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;
}
