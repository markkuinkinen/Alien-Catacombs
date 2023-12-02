using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private static float musicVolume = 0.2f;
    [SerializeField]
    private static float sfxVolume = 0.2f;

    public AudioSource audioPlayer;
    public AudioSource sfxPlayer;
    // BGM
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip storeMusic;

    public Slider musicSlider;
    public Slider sfxSlider;

    // SFX
    public AudioClip gunSound;
    public AudioClip rocketSound;
    public AudioClip explosionSound;
    public AudioClip laserSound;
    public AudioClip eatSound;

    private void Start()
    {
        // Retrieve the saved volume from PlayerPrefs
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
                musicSlider.value = musicVolume;
            }
            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
                sfxSlider.value = sfxVolume;
            }
        }
    }

    public void PlayGunSound(int selectedGun)
    {
        if (selectedGun == 0)
        {
            sfxPlayer.PlayOneShot(gunSound, sfxVolume);
        }
        else if (selectedGun == 1)
        {
            sfxPlayer.PlayOneShot(rocketSound, sfxVolume);
        }
        else if (selectedGun == 2)
        {
            sfxPlayer.PlayOneShot(laserSound, sfxVolume);
        }
        
    }

    public void PlayRocketExplostion()
    {
        sfxPlayer.PlayOneShot(explosionSound, sfxVolume);
    }

    public void PlayEatSound()
    {
        sfxPlayer.PlayOneShot(eatSound, sfxVolume);
    }

    private void Update()
    {
        audioPlayer.volume = musicVolume;
        playMusic();
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            AdjustVolume();
        }
    }

    void AdjustVolume()
    {
        // Update musicVolume only when the slider's value changes
        if (musicSlider.value != musicVolume)
        {
            musicVolume = musicSlider.value;

            // Save the updated volume to PlayerPrefs
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.Save();
        }

        if (sfxSlider.value != sfxVolume)
        {
            sfxVolume = sfxSlider.value;

            // Save the updated volume to PlayerPrefs
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.Save();
        }
    }

    void playMusic()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0)
        {
            if (audioPlayer.clip != menuMusic)
            {
                audioPlayer.clip = menuMusic;
                audioPlayer.Play();
            }
        }
        else if (sceneIndex == 1)
        {
            if (audioPlayer.clip != gameMusic)
            {
                audioPlayer.clip = gameMusic;
                audioPlayer.Play();
            }
        }
        else
        {
            if (audioPlayer.clip != storeMusic)
            {
                audioPlayer.clip = storeMusic;
                audioPlayer.Play();
            }
        }
    }
}