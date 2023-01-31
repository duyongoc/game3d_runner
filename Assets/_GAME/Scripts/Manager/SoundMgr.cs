using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : Singleton<SoundMgr>
{

    [Header("[Setting]")]
    [SerializeField] private SoundConfigSO soundConfig;


    // [private]
    private static AudioSource audioSource;


    // [SFX]
    [HideInInspector] public AudioClip SFX_BACKGROUND;
    [HideInInspector] public AudioClip SFX_CHARACTER_SHIELD;
    [HideInInspector] public AudioClip SFX_CHARACTER_DEAD;



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    // private void Update()
    // {
    // }
    #endregion


    public static void PlaySound(AudioClip audi)
    {
        audioSource.Stop();

        audioSource.clip = audi;
        audioSource.Play();
        audioSource.loop = true;
    }


    public static void StopSound()
    {
        audioSource.Stop();
    }


    public static void PlaySoundOneShot(AudioClip audi)
    {
        StopSound();
        audioSource.PlayOneShot(audi);
    }


    public bool IsPlaying(AudioClip audi)
    {
        return audioSource.clip == audi && audioSource.isPlaying;
    }


    private void CacheDefine()
    {
        SFX_BACKGROUND = soundConfig.sfx_backgound;
        SFX_CHARACTER_SHIELD = soundConfig.sfx_characterShield;
        SFX_CHARACTER_DEAD = soundConfig.sfx_characterDead;
    }


    private void CacheComponent()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
