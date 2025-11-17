using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public enum SETTINGS
{
    MUSIC,
    SFX,
    SENSITIVITY
}

public class Settings : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] SETTINGS settings;
    UnityEvent unityEvent = new UnityEvent();

    private void Start()
    {
        switch(settings)
        {
            case SETTINGS.MUSIC:
                slider.value = SavingLocal.Instance.LocalSave.Music;
                unityEvent.AddListener(UpdateMusic);
                break;

            case SETTINGS.SFX:
                slider.value = SavingLocal.Instance.LocalSave.SFX;
                unityEvent.AddListener(UpdateSFX);
                break;

            case SETTINGS.SENSITIVITY:
                slider.value = SavingLocal.Instance.LocalSave.Sensitivity;
                unityEvent.AddListener(UpdateSensitivity);
                break;
        }
    }

    
    private void UpdateMusic()
    {
        SavingLocal.Instance.LocalSave.Music = slider.value;
        AudioManager.Instance.ChangeMusicVolume(slider.value);
    }
    private void UpdateSFX()
    {
        SavingLocal.Instance.LocalSave.SFX = slider.value;
        AudioManager.Instance.ChangeSFXVolume(slider.value);
    }
    private void UpdateSensitivity()
    {
        SavingLocal.Instance.LocalSave.Sensitivity = slider.value;
    }

    private void Update()
    {
        unityEvent.Invoke();
    }

}
