using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonGeneric<AudioManager>
{
    [SerializeField]
    GameObject Laser;
    [SerializeField]
    GameObject PowerUp;

    AudioSource _laserAudio;
    AudioSource _powerUpAudio;

    void Start()
    {
        _laserAudio = GameObject.Find("Laser").GetComponent<AudioSource>();
        _powerUpAudio = GameObject.Find("Powerup").GetComponent<AudioSource>();
    }

    public void PlayLaserAudio()
    {
        _laserAudio.Play();
    }
    public void PlayPowerUpAudio()
    {
        _powerUpAudio.Play();
    }
}
