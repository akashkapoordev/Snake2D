using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public enum Sounds
{
    BACKGROUNDMUSIC,
   BUTTONSOUND,
   SNAKEGROW,
   SNAKEMOVE,
   SNAKEREDUCE,
   POWERUPS,
   DIE

}
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance {  get { return instance; } }
    public AudioSource soundEffect;
    public AudioSource backGroundMusic;
    
    public SoundType[] soundTypes;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayBackgroundMusic(Sounds.BACKGROUNDMUSIC);
    }

    void PlayBackgroundMusic(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        backGroundMusic.clip = clip;
        backGroundMusic.Play();
    }

    public void SoundEffect(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        soundEffect.PlayOneShot(clip);
    }


    private AudioClip GetAudioClip(Sounds sound)
    {
        SoundType item =   Array.Find(soundTypes, i => i.type == sound);
        if (item != null)
        {
           return item.audioClip;
        }
        return null;
    }


}
