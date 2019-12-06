using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
    }
    public void SetMusicLevel ()
    {
        float sliderValue = musicSlider.value;
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSfxLevel()
    {
        float sliderValue = sfxSlider.value;
        mixer.SetFloat("SfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SfxVolume", sliderValue);
    }

}
