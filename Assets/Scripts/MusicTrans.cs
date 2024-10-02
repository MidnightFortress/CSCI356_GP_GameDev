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
        // just mutes and unmutes it super simple
        // this is for on enter of trigger itll change teh song to the chosen song in ther trigger
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
                // just mutes it and unmutes it, super simple
            }
        }
        // this method is so that we can have the accoustic music play outside of combat and the metal play in it
        // going to be used for combat areas. you enter it plays metal, you leave it plays accoustic
        // taken from https://www.youtube.com/watch?v=SswiJTDagxM
        // music is all my own though. No copyright issues there
    }
}
