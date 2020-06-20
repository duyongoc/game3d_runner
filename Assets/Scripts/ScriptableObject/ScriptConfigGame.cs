using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigGame", menuName = "Config/ConfigGame")]
public class ScriptConfigGame : ScriptableObject
{
    [Header("Skip tutorial")]
    public bool isSkipTutotial = true;

}
