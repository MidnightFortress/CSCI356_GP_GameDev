using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip sound;


    public void PlaySoundOnce()
    {
        if (soundSource != null && sound != null)
        {
            soundSource.PlayOneShot(sound);
        }
        else
        {
            Debug.Log("Check audio source and clip settings!");
        }
    }

    public AudioSource GetSoundSource()
    {
        return soundSource;
    }

    public AudioClip GetAudioClip()
    {
        return sound;
    }
}
