using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TheHole", menuName = "Config/TheHole")]
public class ScriptTheHole : ScriptableObject
{
    [Header("Move speed of the ball")]
    public float moveSpeed = 5f;

    [Header("Distance radirus")]
    public float distanceRadius = 15f;

}
