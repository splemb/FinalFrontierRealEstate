using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionSound : MonoBehaviour
{
    //Components
    AudioSource soundSource;
    public AudioClip sound;

    void Start()
    {
        soundSource = GetComponent<AudioSource>(); //Find audio source
    }

    void OnCollisionEnter(Collision collision)
    {
        soundSource.clip = sound; //Assign sound clip to audio source
        soundSource.Stop(); //Stop currently playing sound
        soundSource.Play(); //Play collision sound
    }
}
