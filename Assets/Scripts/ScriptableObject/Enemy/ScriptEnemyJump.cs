﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyJump", menuName = "Config/EnemyJump")]
public class ScriptEnemyJump : ScriptableObject
{
    [Header("Jump speed enemy 1")]
    public float jumpSpeed = 1f;

    [Header("Jump high enemy 1")]
    public float jumpHigh = 3.5f;

    [Header("Move speed of the enemy 1")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy 1")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 10f;

    [Header("Range to create enemy 1")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy 1")]
    public float distanceWarning = 10f;
}
