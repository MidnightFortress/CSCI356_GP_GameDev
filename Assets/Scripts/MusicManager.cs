using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource currentPlaying = null;

    public AudioSource CurrentSong
    {
        get => currentPlaying;
        set => currentPlaying = value;
    }
    // the music manager to change teh music via trigger
}
