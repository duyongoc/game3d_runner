using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigGame", menuName = "CONFIG/ConfigGame")]
public class ConfigGame : ScriptableObject
{
    [Header("Skip tutorial")]
    public bool isSkipTutotial = true;

    [Header("Moving camera when trigger warning from enemy")]
    public bool isMovingCamera = false;
}
