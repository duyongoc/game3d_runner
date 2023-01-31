using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGlobe", menuName = "CONFIG/Enemy/EnemyGlobe")]
public class ScriptEnemyGlobe : ScriptableObject
{

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;

    [Header("Do we have set up warning for this enemy")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;

    [Header("Number of enemy have warning when spawn")]
    public int numberOfWarning = 3;

    [Header("Move speed of the enemy globe")]
    public float moveSpeed = 3f;

    [Header("Time to spawn enemy globe")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    
    [Header("Range to create enemy globe")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy globe")]
    public float distanceWarning = 5f;
    

}
