using System;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public static event Action<float> MusicVolumed;
    public static event Action<float> SXFVolumed;

    [SerializeField] private Slider m_MusicSlider;
    [SerializeField] private Slider m_SFXSlider;

    private void Awake()
    {
        m_MusicSlider.value = PlayerPrefs.GetFloat("MusicSlider", m_MusicSlider.value);
        m_SFXSlider.value = PlayerPrefs.GetFloat("SFXSlider", m_SFXSlider.value);
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicSlider", m_MusicSlider.value);
        MusicVolumed?.Invoke(m_MusicSlider.value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXSlider", m_SFXSlider.value);
        SXFVolumed?.Invoke(m_SFXSlider.value);
        PlayerPrefs.Save();
    }
}