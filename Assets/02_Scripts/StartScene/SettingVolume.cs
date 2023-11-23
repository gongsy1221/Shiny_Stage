using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = 0;
        sfxSlider.value = 0;
    }

    public void SetBGM()
    {
        float sound = bgmSlider.value;

        if (sound == -40f) mixer.SetFloat("BGM", -80);
        else mixer.SetFloat("BGM", sound);
    }

    public void SetSFX()
    {
        float sound = sfxSlider.value;

        if (sound == -40f) mixer.SetFloat("SFX", -80);
        else mixer.SetFloat("SFX", sound);
    }
}
