using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public AudioMixer mixer;
    public TMPro.TMP_InputField masterVolumeText;
    public TMPro.TMP_InputField musicVolumeText;
    public TMPro.TMP_InputField sfxVolumeText;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterVolumeText.onEndEdit.AddListener(UpdateMasterVolume);
        musicVolumeText.onEndEdit.AddListener(UpdateMusicVolume);
        sfxVolumeText.onEndEdit.AddListener(UpdateSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateMasterVolume()
    {
        masterVolume=(masterVolumeSlider.value);
        UpdateVolumeGraphics();
    }
    public void UpdateMusicVolume()
    {
        musicVolume = (musicVolumeSlider.value);
        UpdateVolumeGraphics();
    }
    public void UpdateSFXVolume()
    {
        sfxVolume=(sfxVolumeSlider.value);
        UpdateVolumeGraphics();
    }
    public void UpdateMasterVolume(string i)
    {
        masterVolume = (Mathf.Clamp(((float)Int32.Parse(i)) / 100f, 0f, 1f));
        UpdateVolumeGraphics();
    }
    public void UpdateMusicVolume(string i)
    {
        musicVolume = (Mathf.Clamp(((float)Int32.Parse(i)) / 100f, 0f, 1f));
        UpdateVolumeGraphics();
    }
    public void UpdateSFXVolume(string i)
    {
        sfxVolume = (Mathf.Clamp(((float)Int32.Parse(i)) / 100f, 0f, 1f));
        UpdateVolumeGraphics();
    }
    private void UpdateVolumeGraphics()
    {
        masterVolumeSlider.value = masterVolume;
        masterVolumeText.text = $"{(int)(masterVolume * 100)}%";
        musicVolumeSlider.value = musicVolume;
        musicVolumeText.text = $"{(int)(musicVolume * 100)}%";
        sfxVolumeSlider.value = sfxVolume;
        sfxVolumeText.text = $"{(int)(sfxVolume * 100)}%";
        UpdateMixerVolume();
    }
    private void UpdateMixerVolume()
    {
        Debug.Log(Mathf.Log(Mathf.Clamp(masterVolume,0.01f,1f))* 20);
        mixer.SetFloat("MasterVolume", Mathf.Log(Mathf.Clamp(masterVolume,0.01f,1f))* 20);
        mixer.SetFloat("MusicVolume", Mathf.Log(Mathf.Clamp(musicVolume,0.01f,1f))* 20);
        mixer.SetFloat("SFXVolume", Mathf.Log(Mathf.Clamp(sfxVolume,0.01f,1f))* 20);
    }
}
