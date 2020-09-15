using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Elastic", menuName = "Config/Elastic")]
public class ScriptElastic : ScriptableObject
{
    [Header("Move speed of the Elastic")]
    public float moveSpeed = 5f;

    [Header("Time to spawn Elastic")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    
    [Header("Range to create Elastic")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;
}
