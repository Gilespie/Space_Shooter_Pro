using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource m_MusicSource;
    [SerializeField] private AudioSource[] m_SFXSources;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetVolume.MusicVolumed += OnChangeMusicVolume;
        SetVolume.SXFVolumed += OnChangeSFXVolume;
    }

    private void OnDestroy()
    {
        SetVolume.MusicVolumed -= OnChangeMusicVolume;
        SetVolume.SXFVolumed -= OnChangeSFXVolume;
    }

    public void OnChangeMusicVolume(float volume)
    {
        m_MusicSource.volume = volume;
    }

    public void OnChangeSFXVolume(float volume)
    {
        for (int i = 0; i < m_SFXSources.Length; i++)
        {
            m_SFXSources[i].volume = volume;
        }
    }

    public void PlaySFX(int id)
    {
        switch(id)
        {
            case 0:
                m_SFXSources[0].Play();
                break;
            case 1:
                m_SFXSources[1].Play();
                break;
            case 2:
                m_SFXSources[2].Play();
                break;
        }
    }
}