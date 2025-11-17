using UnityEngine;

[CreateAssetMenu(fileName = "NewGunStats", menuName = "Weapons/Gun Stats")]
public class GunStats : ScriptableObject
{
    [Header("Munitions")]
    [Tooltip("Nombre de balles par chargeur")]
    public int maxAmmo = 30;

    [Tooltip("Munitions totales disponibles (réserve)")]
    public int totalAmmo = 120;

    [Header("Mode de tir")]
    [Tooltip("Est-ce que l'arme tire en automatique?")]
    public bool isAutomatic = true;

    [Header("Cadence de tir")]
    [Tooltip("Temps entre chaque tir en secondes")]
    public float fireRate = 0.1f;

    [Header("Rechargement")]
    [Tooltip("Temps de rechargement en secondes")]
    public float reloadTime = 2f;

    [Header("Puissance")]
    [Tooltip("Force de la balle")]
    public float bulletStrength = 10f;

    [Tooltip("Dégâts par balle")]
    public int damage = 25;

    [Header("Précision")]
    [Tooltip("Dispersion (0 = parfaitement précis)")]
    [Range(0f, 10f)]
    public float spread = 0.5f;
}