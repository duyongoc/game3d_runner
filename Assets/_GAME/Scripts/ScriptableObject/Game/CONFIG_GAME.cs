using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CONFIG_GAME", menuName = "CONFIG/CONFIG_GAME")]
public class CONFIG_GAME : ScriptableObject
{
    [Header("Skip tutorial")]
    public bool isSkipTutotial = true;

    [Header("Moving camera when trigger warning from enemy")]
    public bool isMovingCamera = false;
}
