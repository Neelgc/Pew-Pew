using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float CurrentTime { get => currentTime; }

    public TextMeshProUGUI textMeshProUGUI;
    
    public bool TimerDown = false;

    public void Init(float timer)
    {
        currentTime = timer;
    }

    internal void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0;
            TimerDown = true;
        }

        string showText = currentTime.ToString("F3");
        textMeshProUGUI.text = showText;
    }
}
