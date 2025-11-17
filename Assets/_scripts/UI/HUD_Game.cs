using TMPro;
using UnityEngine;

public class HUD_Game : MonoBehaviour
{
    public TextMeshProUGUI bulletCount;
    Gun _gun;


    public void InitHUD()
    {
        _gun = FindAnyObjectByType<Gun>();
    }

    public void UpdateHUD()
    {
        bulletCount.text = $"{_gun.GetCurrentAmmo()}/ {_gun.GetTotalAmmo()}";
    }
}
