using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController:MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource FX;

    [Header("Main Menu")]
    public AudioClip buttonSound;

    [Header("Car sounds")]
    public float neutralPitch = 0.5f;
    public void ButtonClip()
    {
        FX.clip = buttonSound;
        FX.Play();
    }

    public void ControlCarMotor(float pitchValue)
    {
        backgroundMusic.pitch = pitchValue;
    }
}

