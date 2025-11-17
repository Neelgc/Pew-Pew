using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    bool _isStart = false;

    [Header("Configuration")]
    [SerializeField] Transform recoilPivot;

    [Header("Recul")]
    [SerializeField] float recoilX = -2f;
    [SerializeField] float recoilY = 0.5f;
    [SerializeField] float recoilZ = 0f; 

    [Header("Retour à la position")]
    [SerializeField] float snappiness = 6f; 
    [SerializeField] float returnSpeed = 4f; 
    private Vector3 currentRecoil;
    private Vector3 targetRecoil;
    private Quaternion originalRotation;

    private void Start()
    {
        if (recoilPivot == null)
        {
            Transform parent = transform.parent;
            while (parent != null)
            {
                if (parent.name.Contains("Camera") || parent.name.Contains("Pivot"))
                {
                    recoilPivot = parent;
                    //Debug.Log($"Pivot de recul trouvé automatiquement : {parent.name}");
                    break;
                }
                parent = parent.parent;
            }

            if (recoilPivot == null)
            {
                Debug.LogWarning("Aucun pivot de recul assigné ! Assignez le Transform parent dans l'Inspector.");
            }
        }
    }

    public void Init()
    {
        _isStart = true;
    }


    private void LateUpdate()
    {
        if (!_isStart) return;

        if (recoilPivot == null) return;

        originalRotation = recoilPivot.localRotation;

        targetRecoil = Vector3.Lerp(targetRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRecoil = Vector3.Slerp(currentRecoil, targetRecoil, snappiness * Time.deltaTime);

        Quaternion recoilRotation = Quaternion.Euler(currentRecoil);
        recoilPivot.localRotation = originalRotation * recoilRotation;
    }

    public void ApplyRecoil()
    {
        targetRecoil += new Vector3(
            -recoilX,
            Random.Range(-recoilY, recoilY),
            Random.Range(-recoilZ, recoilZ)
        );
    }

    public void ResetRecoil()
    {
        currentRecoil = Vector3.zero;
        targetRecoil = Vector3.zero;
    }
}