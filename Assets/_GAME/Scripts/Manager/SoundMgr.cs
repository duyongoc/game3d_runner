using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : Singleton<SoundMgr>
{

    //
    //= inspector
    [SerializeField] private SoundConfigSO soundConfig;


    //
    //= private
    private static AudioSource audioSource;


    //define
    public AudioClip SFX_BACKGROUND;
    public AudioClip SFX_CHARACTER_SHILE;


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

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlaySoundOneShot(AudioClip audi)
    {
        audioSource.PlayOneShot(audi);
    }

    public bool IsPlaying(AudioClip audi)
    {
        return audioSource.clip == audi && audioSource.isPlaying;
    }



    private void CacheDefine()
    {
        SFX_BACKGROUND = soundConfig.sfx_backgound;
        SFX_CHARACTER_SHILE = soundConfig.sfx_characterShield;
    }

    private void CacheComponent()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
