using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"POINTS - {PointManager.Instance.GetPoints().ToString("0000")}";
    }
}
