using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioClip attackClip1;
    public AudioClip attackClip2;
    public AudioClip dialogClip;
    public AudioClip BattleMusic;
    private AudioClip ambientMusic;
    public AudioClip itemClip;
    public AudioClip forceClip;
    public AudioClip defClip;
    public AudioClip HealClip;
    public AudioClip doorClip;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

	}
	
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.volume = 1f;
        efxSource.Play();
    }

    public void PlayAttack()
    {
        RandomizeSfx(attackClip1, attackClip2);
    }

    public void PlayDialog()
    {
        float volume = 0.1f;
        efxSource.clip = dialogClip;
        efxSource.volume = volume;
        efxSource.Play();
    }

    public void PlayBattleMusic()
    {
        musicSource.Stop();
        musicSource.clip = BattleMusic;
        musicSource.Play();
    } 

    public void PlayAmbient()
    {
        musicSource.Stop();
        musicSource.clip = ambientMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayAmbient(AudioClip clip)
    {
        musicSource.Stop();
        ambientMusic = clip;
        musicSource.clip = ambientMusic;
        musicSource.Play();
    }

    public void PlayItem()
    {
        PlaySingle(itemClip);
    }

    public void PlayHeal()
    {
        efxSource.clip = HealClip;
       // efxSource.volume = 0.85f;
        efxSource.Play();

    }

    public void PlayDef()
    {
        PlaySingle(defClip);
    }

    public void playForce()
    {
        PlaySingle(forceClip);
    }

    public void PlayDoor()
    {
        PlaySingle(doorClip);
    }

    private void RandomizeSfx(params AudioClip[] clip)
    {
        int randomIndex = Random.Range(0, clip.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clip[randomIndex];
        efxSource.Play();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
