using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles music playing
[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    //Components
    AudioSource musicSource;
    public AudioClip music;
    public AudioClip finish;

    void Start()
    {
        musicSource = GetComponent<AudioSource>(); //Find audio source
        musicSource.clip = music; //Assign music clip
        musicSource.loop = true; //Set loop to true
        musicSource.Play(); //Play music
    }

    //Triggers win music
    public void WinMusic()
    {
        musicSource.clip = finish; //Assign music clip
        musicSource.loop = false; //Set loop to false
        musicSource.Play(); //Play music
    }
}
