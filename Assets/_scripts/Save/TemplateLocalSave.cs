
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateLocalSave
{
    [SerializeField] int currentWeapon;
    [SerializeField] int levelUnlocked;
    [SerializeField] int points;
    [SerializeField] float music;
    [SerializeField] float sfx;
    [SerializeField] float sensitivity;
    [SerializeField] float[] bestTime;

    public int CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }
    public int LevelUnlocked { get => levelUnlocked; set => levelUnlocked = value; }
    public int Points { get => points; set => points = value; }
    public float Music { get => music; set => music = value; }
    public float SFX { get => sfx; set => sfx = value; }
    public float Sensitivity { get => sensitivity; set => sensitivity = value; }
    public float[] BestTime { get => bestTime; set => bestTime = value; }

    public TemplateLocalSave()
    {
        currentWeapon = 0;
        levelUnlocked = 1;
        points = 0;
        music = 0.5f;
        sfx = 0.5f;
        sensitivity = 10;

        bestTime = new float[12]
        {
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f,
            -1f
        };
    }
}



