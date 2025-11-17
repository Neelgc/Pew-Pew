using UnityEngine;

public class LevelManager : MonoBehaviour
{

    static LevelManager _instance;
    public static LevelManager Instance {  get { return _instance; } }

    int unlockLevel = 1;
    int maxLevel = 12;

    public int UnlockLevel { get { return unlockLevel; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        unlockLevel = SavingLocal.Instance.LocalSave.LevelUnlocked;
    }

    public void UnlockNewLevel(int levelFinished)
    {
        Debug.Log(levelFinished);
        if (levelFinished != unlockLevel)
            return;

        unlockLevel = unlockLevel > maxLevel ? 12 : unlockLevel + 1;
        SavingLocal.Instance.LocalSave.LevelUnlocked = unlockLevel;
    }

}
