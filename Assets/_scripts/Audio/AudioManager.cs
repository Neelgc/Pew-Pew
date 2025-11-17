using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;
    public static AudioManager Instance { get => _instance; }

    [SerializeField] List<Sound> music;
    [SerializeField] List<Sound> SFX;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        transform.SetParent(null);

        PlayMusic("Theme");

        musicSource.volume = SavingLocal.Instance.LocalSave.Music;
        SFXSource.volume = SavingLocal.Instance.LocalSave.SFX;
    }

    public void PlayMusic(string nameMusic)
    {
        Sound s = music.Find(sound => sound.name.Equals(nameMusic));

        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.mute = s.mute;
            musicSource.loop = s.loop;

            musicSource.Play();
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string nameSFX)
    {
        Sound s = SFX.Find(sound => sound.name.Equals(nameSFX));

        if (s != null)
        {
            SFXSource.PlayOneShot(s.clip);
        }
    }
    public void ChangeSFXVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
