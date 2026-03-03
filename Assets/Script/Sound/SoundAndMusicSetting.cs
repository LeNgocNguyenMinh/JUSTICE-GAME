using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundAndMusicSetting : MonoBehaviour
{
    public static SoundAndMusicSetting Instance;
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField]private Slider musicSlider;
    [SerializeField]private Slider sfxSlider;
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
    public void SetStartValue()
    {
        SetMusicVolume();
        SetSFXVolume();
    }
    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("musicParam", Mathf.Log10(musicVolume)*20);
    }
    public void SetSFXVolume()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("sfxParam", Mathf.Log10(sfxVolume)*20);
    }
}
