using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrans : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource musicToChange = null;
    [SerializeField] private AudioSource musicDefault;

    private void OnTriggerEnter(Collider other)
    {
        bool isAPlayer = other.gameObject.CompareTag("Player");
        if (isAPlayer)
        {
            ChangeSong();
        }
    }

    private void ChangeSong()
    {
        MusicManager musicMe = FindAnyObjectByType<MusicManager>();
        if (musicMe == null)  return;
        if (musicMe.CurrentSong == null)
        {
            musicMe.CurrentSong = musicToChange;
            musicMe.CurrentSong.mute = false;
            return;
        }
        musicMe.CurrentSong.mute = true;
        musicMe.CurrentSong = musicToChange;
        musicMe.CurrentSong.mute = false;
    }

    private void OnTriggerExit(Collider other)
    {
        MusicManager musicMe = FindAnyObjectByType<MusicManager>();
       bool ContinuedPlaying = musicMe.CurrentSong.gameObject.CompareTag("Accoustic");
        Debug.Log("Bool music playing: " +  ContinuedPlaying);
        bool isAPlayer = other.gameObject.CompareTag("Player");
        if (isAPlayer)
        {
            if (ContinuedPlaying) return;
            else if (!ContinuedPlaying)
            {
                musicMe.CurrentSong.mute = true;
                musicMe.CurrentSong = musicDefault;
                musicMe.CurrentSong.mute = false;
            }
        }
        // this method is so that we can have the accoustic music play outside of combat and the metal play in it
    }
}
