using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meteorite", menuName = "Config/Meteorite")]
public class ScriptMeteorite : ScriptableObject
{
    [Header("Time to spawn enemy 5")]
    public float timeSpawn = 3f;
    public float timeProcessSpawn = 2.5f;

    [Header("Range to create enemy 5")]
    public float minRangeSpawn = 15f;
    public float maxRangeSpawn = 30f;

    [Header("Time delay spawn when game start")]
    public float timeDelay = 30f;
}
