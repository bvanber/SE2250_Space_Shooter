using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip shotSfx;
    public AudioClip crashSfx;          //Adding in sound variables
    public AudioSource sourceShot;
    public AudioSource sourceCrash;
    public static SoundManager soundManager;
    void Awake()
    {
        sourceCrash = GetComponent<AudioSource>();
        sourceShot = GetComponent<AudioSource>(); 
        soundManager = GetComponent<SoundManager>();
    }
    public void shotSound()
    {
        sourceShot.PlayOneShot(shotSfx,1);              //creating functions to play sounds
    }
    public void crashSound()
    {
        sourceCrash.PlayOneShot(crashSfx,1);
    }

}
