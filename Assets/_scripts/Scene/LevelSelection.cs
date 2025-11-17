using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
     [SerializeField] GameObject MenuButton;

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name == "MainMenu")
            {
                MenuButton.SetActive(false);
                return;
            }
        }
    }
}
