using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelection : MonoBehaviour
{
    int currentWeapon;
    public GameObject CurrentWeapon {  get { return gunList[currentWeapon]; } }

    [SerializeField] List<GameObject> gunList;
    List<Image> buttonList;

    Dictionary<bool, Color> ColorMatch = new Dictionary<bool, Color>
    {
        {true ,  new Color(0.6784314f, 1f, 0.1843137f, 1f) },
        {false , new Color(0.2745098f, 0.509804f, 0.7058824f, 0.3f) },
    };

    private void Start()
    {
        currentWeapon = SavingLocal.Instance.LocalSave.CurrentWeapon;

        Button[] b = transform.GetComponentsInChildren<Button>();
        buttonList = new List<Image>();
        foreach (Button b2 in b)
        {
            buttonList.Add(b2.GetComponent<Image>());
        }

        foreach (var item in buttonList)
        {
            item.color = ColorMatch[false];
        }
        buttonList[currentWeapon].color = ColorMatch[true];
    }

    public void SelectGun(int id)
    {
        foreach (var item in buttonList)
        {
            item.color = ColorMatch[false];
        }
        currentWeapon = id;
        buttonList[id].color = ColorMatch[true];

        SavingLocal.Instance.LocalSave.CurrentWeapon = currentWeapon;
    }


}
