using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public string _sceneName;
    public LoadSceneMode _loadMode;
    public bool isMainMenu;

    private void Awake()
    {
        if (isMainMenu) SceneManager.LoadScene(_sceneName, _loadMode);
    }

    public void LoadScene()
    {
        if (_sceneName == "MainMenu")
        {
            AudioManager.Instance.PlayMusic("Theme");
        }
        SceneManager.LoadScene(_sceneName, _loadMode);
    }
    public void LoadScene(string sceneName, LoadSceneMode loadMode)
    {
        if(sceneName == "MainMenu") 
        {
            AudioManager.Instance.PlayMusic("Theme");
        }
        SceneManager.LoadScene(sceneName, loadMode);
    }
    public void UnloadScene(string sceneName)
    {
        GameManager gm = GameObject.FindAnyObjectByType<GameManager>();
        if(gm != null)
        {
            if (gm.IsWin == StateWinLoose.None) {
                gm.OpenLevelSelection();
                return;
            }
        }
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void SwapScene(string scene)
    {
        if(_sceneName == SceneManager.GetActiveScene().name)
        {
            SceneManager.UnloadSceneAsync(_sceneName);
            SceneManager.LoadScene(scene);
        }
        else
        {
            SceneManager.UnloadSceneAsync(scene);
            SceneManager.LoadScene(_sceneName);
        }
    }
}
