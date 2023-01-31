using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CONFIG_Sound", menuName = "CONFIG/CONFIG_Sound")]
public class SoundConfigSO : ScriptableObject
{
    
    [Header("Game")]
    public AudioClip sfx_backgound;

    [Header("Character")]
    public AudioClip sfx_characterShield;
    public AudioClip sfx_characterDead;


}
