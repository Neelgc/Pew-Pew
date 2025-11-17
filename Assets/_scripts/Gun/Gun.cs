using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject ApparitionBullet;
    [SerializeField] GameObject Bullet;
    [SerializeField] GunStats gunStats;
    [SerializeField] GunRecoil gunRecoil;
    [SerializeField] Animator weaponAnimator; 

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;

    [Header("Visual Effects")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] Transform shellEjectionPoint;

    private int currentAmmo;
    private int totalAmmo;
    private float nextFireTime = 0f;
    private bool isReloading = false;

    InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.Instance;

        if (gunStats != null)
        {
            currentAmmo = gunStats.maxAmmo;
            totalAmmo = gunStats.totalAmmo;
        }

        if (gunRecoil == null)
        {
            gunRecoil = GetComponent<GunRecoil>();
        }
    }

    internal void UpdateGun()
    {
        if (_inputManager == null)
        {
            Debug.Log("Input NULL");
            _inputManager = InputManager.Instance;
            return;
        }
        if (gunStats == null) return;

        if (_inputManager.PlayerReloadThisFrame() && !isReloading && currentAmmo < gunStats.maxAmmo && totalAmmo > 0)
        {
            StartReload();
            return;
        }

        weaponAnimator.SetBool("Scope", _inputManager.Scope());

        if (currentAmmo <= 0 && !isReloading && totalAmmo > 0)
        {
            StartReload();
            return;
        }

        bool shootInput = gunStats.isAutomatic ? _inputManager.PlayerShootHeld() : _inputManager.PlayerShootThisFrame();

        if (shootInput && Time.time >= nextFireTime && !isReloading && currentAmmo > 0)
        {
            AudioManager.Instance.PlaySFX(gunStats.isAutomatic ? "M4Shoot" : "PistolShoot");
            Shoot();
            nextFireTime = Time.time + gunStats.fireRate;
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("Shoot");
            }

            Quaternion bulletRotation = ApparitionBullet.transform.rotation;

            if (gunStats.spread > 0)
            {
                Vector3 spread = new Vector3(
                    Random.Range(-gunStats.spread, gunStats.spread),
                    Random.Range(-gunStats.spread, gunStats.spread),
                    0f
                );
                bulletRotation *= Quaternion.Euler(spread);
            }

            GameObject bulletInstance = Instantiate(Bullet, ApparitionBullet.transform.position, bulletRotation);

            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.strength = gunStats.bulletStrength;
            }

            if (gunRecoil != null)
            {
                gunRecoil.ApplyRecoil();
            }

            currentAmmo--;

            //Debug.Log($"Munitions: {currentAmmo}/{gunStats.maxAmmo} | R�serve: {totalAmmo}");
        }
    }

    private void StartReload()
    {
        if (!isReloading && totalAmmo > 0)
        {
            isReloading = true;
            Debug.Log("Rechargement...");

            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("Reload");
            }

            Invoke(nameof(FinishReload), gunStats.reloadTime);
        }
    }

    private void FinishReload()
    {
        int ammoNeeded = gunStats.maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);

        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;

        isReloading = false;
        Debug.Log($"Rechargement termin�! Munitions: {currentAmmo}/{gunStats.maxAmmo} | R�serve: {totalAmmo}");
    }

    public void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void PlayReloadSound()
    {
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }
    }


    public void EjectShell()
    {
        if (shellPrefab != null && shellEjectionPoint != null)
        {
            GameObject shell = Instantiate(shellPrefab, shellEjectionPoint.position, shellEjectionPoint.rotation);

            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            if (shellRb != null)
            {
                Vector3 ejectionForce = shellEjectionPoint.right * Random.Range(0.5f, 1f) + shellEjectionPoint.up * Random.Range(0.3f, 0.6f);

                shellRb.AddForce(ejectionForce, ForceMode.Impulse);

                shellRb.AddTorque(Random.insideUnitSphere * 3f, ForceMode.Impulse);
            }
        }
    }

    public void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }

    public void ReturnIdle()
    {
        weaponAnimator.SetTrigger("GoIdle");
    }


    public int GetCurrentAmmo() => currentAmmo;
    public int GetMaxAmmo() => gunStats != null ? gunStats.maxAmmo : 0;
    public int GetTotalAmmo() => totalAmmo;
    public bool IsReloading() => isReloading;
    public GunStats GetGunStats() => gunStats;

    internal void Init()
    {
        _inputManager = InputManager.Instance;
    }

    public void MakeSFX(string nameSFX)
    {
        AudioManager.Instance.PlaySFX(nameSFX);
    }
}