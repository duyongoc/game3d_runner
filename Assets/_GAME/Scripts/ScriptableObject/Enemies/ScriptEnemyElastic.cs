using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyElastic", menuName = "CONFIG/Enemy/EnemyElastic")]
public class ScriptEnemyElastic : ScriptableObject
{

    [Header("CONFIG")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;
    public int numberOfWarning = 3;

    [Header("Enemy's param")]
    public float timeDelay = 30f;
    public float distanceAttack = 5f;
    public float timeCharge = 1.5f;
    public float timeAttack = 2f;
    public float moveSpeed = 5f;

    [Header("Enemy' spawning")]
    public float timeToSpawn = 3f;
    public float timeProcessSpawn = 2.5f;
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Other")]
    public Material marDissolve;
    public GameObject prefabExplosion;
    public GameObject prefabPrepareAttack;

}
