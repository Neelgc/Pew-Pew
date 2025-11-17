using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuActions : MonoBehaviour
{


    public void Continue()
    {
        string levelSceneName = "Level_" + LevelManager.Instance.UnlockLevel.ToString();
        Debug.Log(levelSceneName);
        SceneManager.LoadSceneAsync(levelSceneName);
        SceneManager.LoadSceneAsync("Game_HUD", LoadSceneMode.Additive);
    }
    public void Restart()
    {
        string levelSceneName = "Level_" + GameManager.Instance.numLevel;
        Debug.Log(levelSceneName);
        SceneManager.LoadSceneAsync(levelSceneName);
        SceneManager.LoadSceneAsync("Game_HUD", LoadSceneMode.Additive);
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync("Settings",LoadSceneMode.Additive);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
