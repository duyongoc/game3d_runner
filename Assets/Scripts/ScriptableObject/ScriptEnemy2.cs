using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy2", menuName = "Config/Enemy2")]
public class ScriptEnemy2 : ScriptableObject
{
    [Header("Move speed of the Enemy 2")]
    public float moveSpeed = 5f;
}
