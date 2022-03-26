using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioMixer musicMixer;

    public AudioSource backgroundMusic;

    public static AudioManager instance;


    public float masterVol;
    public Slider masterSldr;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(backgroundMusic);
        masterSldr.value = masterVol;

        masterSldr.minValue = -80;
        masterSldr.maxValue = 10;

    }

    // Update is called once per frame
    void Update()
    {
        MasterVolume();
    }

    public void MasterVolume()
    {
        musicMixer.SetFloat("masterVolume", masterSldr.value);
    }


    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }


}
