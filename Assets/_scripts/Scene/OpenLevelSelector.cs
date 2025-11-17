using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevelSelector : MonoBehaviour
{
    public LoadingScene loadSceneManager;
    
    public string sceneName;
    public bool isOpen = false;

    public bool changeIsOpen = false;

    public void OpenLevelSelect()
    {
        if(loadSceneManager == null)
            loadSceneManager = GameObject.FindAnyObjectByType<LoadingScene>();

        if (!isOpen)
        {
            loadSceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            isOpen = changeIsOpen ? isOpen : true;
        }
        else
        {
            loadSceneManager.UnloadScene(sceneName);
            isOpen = changeIsOpen ? isOpen : false;
        }

    }
}
