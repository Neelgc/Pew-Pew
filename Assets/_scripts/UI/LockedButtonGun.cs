using UnityEngine;

public class LockedButtonGun : MonoBehaviour
{
    [SerializeField] int neededPoint;

    private void Start()
    {
        if( PointManager.Instance.GetPoints() >= neededPoint)
        {
            Destroy(gameObject);
        }
    }
}
