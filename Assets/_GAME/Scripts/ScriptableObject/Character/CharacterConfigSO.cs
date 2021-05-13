using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CONFIG_CHARACTER", menuName = "CONFIG/Character/CONFIG_CHARACTER")]
public class CharacterConfigSO : ScriptableObject
{

    [Header("Character's param", order = 1)]
    [Header("Character speed", order = 2)]
    public GameObject prefabMovingParticle;
    public GameObject prefabMovingTrail;
    public float moveSpeed = 5f;
    public float angleSpeed = 100;
    public float timeParticleMove = 0.05f;


    [Header("Other speed ")]
    public float speedIncrease = 3f;
    public float timerSpeedUp = 3.5f;
    public float engineForce = 1.7f;
    public float turnSpeed = 170f;

    [Header("Character shield")]
    public GameObject prefabShield;
    public float timeShield = 12f;
    public float timeShieldFinish = 8f;


}
