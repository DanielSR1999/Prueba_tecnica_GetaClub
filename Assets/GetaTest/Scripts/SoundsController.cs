using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController:MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource FX;
    public AudioSource Drifting;

    [Header("Main Menu")]
    public AudioClip buttonSound;

    [Header("Car sounds")]
    public float neutralPitch = 0.5f;

    [Header("Game")]
    public AudioClip winSound;
    public AudioClip lossSound;
    public void ButtonClip()
    {
        FX.clip = buttonSound;
        FX.Play();
    }

    public void ControlCarMotor(float pitchValue)
    {
        backgroundMusic.pitch = pitchValue;
    }
    public void PlayWin()
    {
        FX.clip = winSound;
        FX.Play();
    }
    public void PlayLoss()
    {
        FX.clip = lossSound;
        FX.Play();
    }
    
}

