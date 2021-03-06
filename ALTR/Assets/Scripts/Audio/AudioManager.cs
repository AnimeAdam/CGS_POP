﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    LevelID levelIdentity;
    [Header ("Sound Effects")]
	public AudioSource Grow;
	public AudioSource Pull;
	public AudioSource Switch_1;
	public AudioSource Switch_2;
	public AudioSource Switch_3;
	public AudioSource Float_Up;
	public AudioSource FloatRelease;
	public AudioSource Flag_1;
	public AudioSource Flag_2;
	public AudioSource Flag_3;
	public AudioSource Flag_4;
	public AudioSource Jump;
	public AudioSource Land;
	public AudioSource Step_1;
	public AudioSource Step_2;
	public AudioSource Step_3;
    public AudioSource Wall_Hit;
    public AudioSource GlassSmash;
    public AudioSource RespawnSFX;
    //public AudioSource abilitySound;
    //public AudioSource levelMusic;
    //public AudioSource effectsSound;
	public AudioSource Circular_Saw;
    public AudioSource PauseSound;
    public AudioSource MenuSound;
    public AudioSource DoorUnlock;

    [Header("Music")]
    public AudioSource[] TestMusic = new AudioSource[5];


    [Header("Audio Manager")]
    public static AudioManager instance = null;
    //[SerializeField] List<AudioClip> abilityClips;
    //[SerializeField] List<AudioClip> musicClips;
    //[SerializeField] List<AudioClip> effectClips;

    private void Awake()
    {
        levelIdentity = FindObjectOfType<LevelID>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        TestMusic[levelIdentity.musicTrack].Play();
    }
}
