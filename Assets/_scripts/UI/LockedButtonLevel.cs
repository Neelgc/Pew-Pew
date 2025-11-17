using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockedButtonLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] Image image;
    [SerializeField] GameObject Locker;

    private void Awake()
    {
        int numLvl = int.Parse(m_TextMeshProUGUI.text);
        if (LevelManager.Instance.UnlockLevel >= numLvl)
        {
            if (SavingLocal.Instance.LocalSave.BestTime[numLvl -1] >= 0)
                image.color = Color.greenYellow;
            Locker.SetActive(false);
        }
        else
            Locker.SetActive(true);



    }

}
