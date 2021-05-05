using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr s_instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if(s_instance != null)
            return;
        
        s_instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundOneShot(AudioClip audi)
    {
        audioSource.PlayOneShot(audi);
    }
    
    public void PlaySound(AudioClip audi)
    {
        audioSource.Stop();

        audioSource.clip = audi;
        audioSource.Play();
        audioSource.loop = true;
    }

    public bool IsPlaying(AudioClip audi)
    {
        return audioSource.clip == audi && audioSource.isPlaying;
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public static SoundMgr GetInstance()
    {
        return s_instance;
    }

    

}
