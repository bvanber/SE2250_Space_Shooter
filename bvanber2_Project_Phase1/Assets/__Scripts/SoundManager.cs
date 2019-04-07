using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip shotSfx;
    public AudioClip levelUpSfx;
    public AudioClip powerUpSfx;
    public AudioClip gameStartSfx;
    public AudioClip crashSfx;          //Adding in sound variables
    public AudioSource source;

    public static SoundManager soundManager;
    void Awake()
    {
        source = GetComponent<AudioSource>();

        soundManager = GetComponent<SoundManager>();
    }
    public void LevelUpSound()
    {

        source.PlayOneShot(levelUpSfx, 0.2f);

    }
    public void GameStartSound()
    {

        source.PlayOneShot(gameStartSfx, 0.2f);

    }
    public void ShotSound()
    {
        source.PlayOneShot(shotSfx, 1);              //creating functions to play sounds
    }
    public void CrashSound()
    {
        source.PlayOneShot(crashSfx, 1);
    }
    public void PowerUpSound()
    {
        source.PlayOneShot(powerUpSfx, 1);
    }


}