using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefault", menuName = "CONFIG/Enemy/EnemyDefault")]
public class ScriptEnemyDefault : ScriptableObject
{

    [Header("Enemy's warning")]
    public SetUp.Warning setWarning = SetUp.Warning.Disabe;
    public float distanceWarning = 8f;

    [Header("Enemy param")]
    public float timeSpawn = 3f;
    public float moveSpeed = 5f;

    [Header("Range to create enemy default")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Other")]
    public Material marDissolve;
    public GameObject prefabExplosion;
    public GameObject prefabExplosionSpecial;



}
