using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy1", menuName = "Config/Enemy1")]
public class ScriptEnemy1 : ScriptableObject
{
    [Header("Jump speed enemy 1")]
    public float jumpSpeed = 1f;

    [Header("Jump high enemy 1")]
    public float jumpHigh = 3.5f;

    [Header("Move speed of the Enemy 1")]
    public float moveSpeed = 5f;

    [Header("Time to spawn Enemy 1")]
    public float timeSpawn = 3f;

}
