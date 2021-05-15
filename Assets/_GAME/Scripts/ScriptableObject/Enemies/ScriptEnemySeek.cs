using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySeek", menuName = "CONFIG/Enemy/EnemySeek")]
public class ScriptEnemySeek : ScriptableObject
{

    [Header("Enemy's param")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;
    public float distanceWarning = 8f;
    public int numberOfWarning = 3;
    [Range(0.1f, 5f)]
    public float slowdownTurning = 0.1f;
    public float moveSpeed = 7f;

    [Header("EnemySpawning")]
    public float timeDelay = 30f;
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    public float timeTurning = 0.1f;

    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Other")]
    public Material marDissolve;
    public GameObject prefabExplosion;
    public GameObject prefabExplosionSpecial;
    public GameObject prefabMoveTurning;


}
