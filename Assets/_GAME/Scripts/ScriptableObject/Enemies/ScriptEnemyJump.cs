using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyJump", menuName = "CONFIG/Enemy/EnemyJump")]
public class ScriptEnemyJump : ScriptableObject
{

    [Header("Do we have set up warning for this enemy")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;
    public int numberOfWarning = 3;
    public float distanceWarning = 10f;


    [Header("Enemy's param", order = 2)]
    public float moveSpeed = 5f;
    public float jumpSpeed = 1f;
    public float jumpHigh = 3.5f;
    public float distanceAttack = 5f;

    [Header("Enemies spawning")]
    public float timeToSpawn = 3f;
    public float timeProcessSpawn = 2.5f;
    public float timeDelay = 10f;
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Other")]
    public Material marDissolve;
     public GameObject prefabExplosion;
    public GameObject prefabJumpExplosion;
    public GameObject shapeArlet;

    
}
