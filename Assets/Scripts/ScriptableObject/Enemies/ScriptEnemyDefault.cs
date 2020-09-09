using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefault", menuName = "Config/EnemyDefault")]
public class ScriptEnemyDefault : ScriptableObject
{
    [Header("Do we have set up warning for this enemy")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;

    //[Header("Number of enemy have warning when spawn")]
    //public int numberOfWarning = 0;
    
    [Header("Move speed of the enemy default")]
    public float moveSpeed = 5f;

    [Header("Time to spawn enemy default")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    
    [Header("Range to create enemy default")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Distance trigger warning from enemy default")]
    public float distanceWarning = 8f;
}
