using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioClip sound2;

    public float sound2Delay = 0.5f;

    public void PlaySoundOnce()
    {
        if (soundSource != null && sound != null)
        {
            soundSource.PlayOneShot(sound);
            if (sound2 != null)
            {
                Invoke(nameof(PlaySound2), sound2Delay);
            }
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

    void PlaySound2()
    {
        soundSource.PlayOneShot(sound2);
    }
}
