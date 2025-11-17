using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SavingLocal : MonoBehaviour
{
    static SavingLocal _instance;

    public TemplateLocalSave _localSave;
    string _fileName = "Data";

    public static SavingLocal Instance {  get { return _instance; } }
    public TemplateLocalSave LocalSave { get { return _localSave; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        transform.SetParent(null);

        if (JsonManager.HaveJson(_fileName))
        {
            _localSave = JsonManager.Load<TemplateLocalSave>(_fileName);
        }
        else
        {
            _localSave = new TemplateLocalSave();
            JsonManager.Save(_fileName, _localSave);
        }
    }

    public void Init()
    {

    }

    public void SaveLocalData()
    {
        JsonManager.Save(_fileName, _localSave);
        Debug.Log("Save");
    }

    public void ResetData()
    {

    }
}
