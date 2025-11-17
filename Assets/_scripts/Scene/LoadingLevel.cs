using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingLevel : MonoBehaviour
{
    public void LoadLevel(int numLevel)
    {
        string levelSceneName = "Level_" + numLevel.ToString();
        SceneManager.LoadSceneAsync(levelSceneName);
        SceneManager.LoadSceneAsync("Game_HUD", LoadSceneMode.Additive);
    }
}
