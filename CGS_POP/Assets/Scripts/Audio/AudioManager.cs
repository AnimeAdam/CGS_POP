using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource abilitySound;
	public AudioSource levelMusic;
	public AudioSource effectsSound;
	public static AudioManager instance = null;
	[SerializeField] List<AudioClip> abilityClips;
	[SerializeField] List<AudioClip> musicClips;
	[SerializeField] List<AudioClip> effectClips;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

	}

	public void PlayAbilitySound(AudioClip soundClip) {
		abilitySound.clip = soundClip;
		abilitySound.Play();
	}

	public void PlayEffectSound(AudioClip effectClip){
		effectsSound.clip = effectClip;
		effectsSound.Play();
	}

	public void PlayLevelMusic(AudioClip levelMusicClip)
	{
		levelMusic.clip = levelMusicClip;
		levelMusic.Play();
		levelMusic.loop = true;
	}
}
