using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StateWinLoose
{
    None,
    Win,
    Lose
}

public class GameManager : MonoBehaviour
{
    InputManager inputManager;

    public static GameManager Instance;

    public Transform gunParent;

    Timer timer;
    PlayerController playerController;
    Gun gun;
    Camera_FPS cam_FPS;
    HUD_Game HUD;

    [SerializeField] GunSelection gunSelection;

    public Transform targetParent;
    int nbTarget;

    bool isMenuOpen = false;
    [SerializeField] GameObject waitingScreen;
    bool levelStart = false;
    [HideInInspector]public int numLevel;
    bool haveAlreadyWin;

    StateWinLoose isWin = StateWinLoose.None;
    public StateWinLoose IsWin {  get { return isWin; } }

    Dictionary<int, (float, int)> gmDict = new Dictionary<int, (float, int)>()
    {
        {1,  (60,100) },
        {2,  (40,100) },
        {3,  (40,100) },
        {4,  (30,100) },
        {5,  (30,100) },
        {6,  (45,100) },
        {7,  (30,100) },
        {8,  (40,100) },
        {9 , (60,100) },
        {10, (30,100) },
        {11, (60,100) },
        {12, (35,100) },
    };
    private void Start()
    {
        inputManager = InputManager.Instance;
        Instance = this;
        numLevel = int.Parse(SceneManager.GetActiveScene().name.Replace("Level_", ""));
        haveAlreadyWin = (int)SavingLocal.Instance.LocalSave.BestTime[numLevel-1] != -1;

        AudioManager.Instance.StopMusic();  

    }

    private void Update()
    {
        if (!levelStart)
        {
            CheckGameStart();
            return;
        }

        switch (isWin)
        {
            case StateWinLoose.None:
                if (inputManager.OpenMenu())
                {
                    OpenLevelSelection();
                    return;
                }
                if (isMenuOpen)
                    return;

                playerController.UpdatePlayer();
                gun.UpdateGun();
                cam_FPS.UpdateCam();
                timer.UpdateTimer();
                HUD.UpdateHUD();

                CheckLoose();
                
                break;
            case StateWinLoose.Win:
                break;
            case StateWinLoose.Lose:
                break;
        }


    }

    private void CheckGameStart()
    {
        levelStart = inputManager.PlayerStartGame();
        if (levelStart)
        {
            Destroy(waitingScreen);
            InitGame();

            AudioManager.Instance.PlayMusic("InGame");

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

    public void OpenLevelSelection()
    {
        if (isMenuOpen)
        {
            SceneManager.UnloadSceneAsync("LevelSelection");
            Cursor.visible = false;
        }
        else
        {
            SceneManager.LoadScene("LevelSelection", LoadSceneMode.Additive);
            Cursor.visible = true;
        }
        isMenuOpen = !isMenuOpen;
    }

    void InitGame()
    {
        timer = FindAnyObjectByType<Timer>();
        timer.Init(gmDict[numLevel].Item1);

        playerController = FindAnyObjectByType<PlayerController>();

        GameObject g = Instantiate(gunSelection.CurrentWeapon, gunParent);
        gun = g.GetComponent<Gun>();
        gun.Init();

        FindAnyObjectByType<GunRecoil>().Init();

        HUD = FindAnyObjectByType<HUD_Game>();
        HUD.InitHUD();

        cam_FPS = FindAnyObjectByType<Camera_FPS>();
        cam_FPS.Init();

        nbTarget = targetParent.childCount;
        Debug.Log(nbTarget);
    }

    public void CheckWin(int minusTarget = 1)
    {
        nbTarget -= minusTarget;
        isWin = nbTarget == 0 ? StateWinLoose.Win : isWin;
        if(isWin == StateWinLoose.Win)
        {
            AudioManager.Instance.PlayMusic("Win");
            Cursor.visible = true;
            if(numLevel != 12)
                LevelManager.Instance.UnlockNewLevel(int.Parse(SceneManager.GetActiveScene().name.Replace("Level_","")));
            if (!haveAlreadyWin)
            {
                PointManager.Instance.AddPoint(gmDict[numLevel].Item2);
            }

            if (timer.CurrentTime > SavingLocal.Instance.LocalSave.BestTime[numLevel-1])
            {
                SavingLocal.Instance.LocalSave.BestTime[numLevel - 1] = timer.CurrentTime;
            }

            SavingLocal.Instance.SaveLocalData();

            SceneManager.LoadScene("End_UI", LoadSceneMode.Additive);


        }
    }

    void CheckLoose()
    {
        isWin = timer.TimerDown ? StateWinLoose.Lose : isWin;
        if(isWin == StateWinLoose.Lose)
        {
            Cursor.visible = true;
            AudioManager.Instance.PlayMusic("Loose");
            SceneManager.LoadScene("End_UI", LoadSceneMode.Additive);
        }
    }

    public int GetPointFromCurrentLevel(bool CheckIfFirstWin = false)
    {
        if(CheckIfFirstWin)
        {
            if (haveAlreadyWin)
            {
                return 0;
            }
            return gmDict[numLevel].Item2;
        }
        else
        {
            return gmDict[numLevel].Item2;
        }

    }
}
