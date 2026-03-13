using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControl : MonoBehaviour
{
    public static SoundControl Instance;
    [SerializeField]private AudioSource musicSrc;
    [SerializeField]private AudioSource sfxSrc;
    [Header("----------Game Music----------")]
    [SerializeField]public AudioClip mainMenuMusic;
    [SerializeField]public AudioClip levelSelectMusic;
    [SerializeField]public AudioClip inGameMusic;
    [Header("----------Player Audio Clips----------")]
    [SerializeField]public AudioClip playerSwordSheathSound;
    [SerializeField]public AudioClip playerSwordDeflectSound;
    [SerializeField]public AudioClip playerDeflectTingSound;
    [SerializeField]public AudioClip hitSound;
    [Header("----------Intro sound----------")]
    [SerializeField]private AudioClip windBlowSound;
    [SerializeField]private AudioClip leafWalkSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }   
    }
    public void PlayerSwordSheathSoundPlay()
    {
        PlaySFX(playerSwordSheathSound);
    }
    public void PlayerSwordDeflectSoundPlay()
    {
        PlaySFX(playerSwordDeflectSound);
    }
    public void PlayerDeflectTingSoundPlay()
    {
        PlaySFX(playerDeflectTingSound);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }
    public void HitSoundPlay()
    {
        PlaySFX(hitSound);
    }
    public void WindBlowSoundPlay()
    {
        PlaySFX(windBlowSound);
    }
    public void LeafWalkSoundPlay()
    {
        PlaySFX(leafWalkSound);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSrc.clip = clip;
        musicSrc.loop = true;
        musicSrc.Play();
    }
    public void InGameMusicPlay()
    {
        PlayMusic(inGameMusic);
    }
    public void MainMenuMusicPlay()
    {
        PlayMusic(mainMenuMusic);
    }
    public void LevelSelectMusicPlay()
    {
        PlayMusic(levelSelectMusic);
    }
}
