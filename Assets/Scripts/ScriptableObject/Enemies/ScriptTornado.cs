using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tornado", menuName = "Config/Tornado")]
public class ScriptTornado : ScriptableObject
{
    [Header("Move speed of the Tornado")]
    public float moveSpeed = 5f;

    [Header("Time to spawn Tornado")]
    public float timeProcessSpawn = 2.5f;
    public float timeSpawn = 3f;
    
    [Header("Range to create Tornado")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;
}
