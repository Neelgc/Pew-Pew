using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WinLooseAssign : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI win_loose;

    [SerializeField] TextMeshProUGUI bestTime;

    [SerializeField] TextMeshProUGUI currentTime;

    [SerializeField] TextMeshProUGUI pointText;

    [SerializeField] Button continuButton;


    private void Start()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm.IsWin == StateWinLoose.Win)
        {
            win_loose.text = "WIN";
            continuButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUE";
            continuButton.onClick.AddListener(continuButton.GetComponent<ButtonMenuActions>().Continue);

            int point = GameManager.Instance.GetPointFromCurrentLevel(true);
            if (point > 0)
                pointText.text = $"YOU GAIN {GameManager.Instance.GetPointFromCurrentLevel()} POINTS";
            else
                pointText.text = "";
        }
        else
        {
            win_loose.text = "LOOSE";
            continuButton.GetComponentInChildren<TextMeshProUGUI>().text = "RESTART";
            continuButton.onClick.AddListener(continuButton.GetComponent<ButtonMenuActions>().Restart);
            pointText.text = "";
        }

        bestTime.text = $"BEST TIME  -  {SavingLocal.Instance.LocalSave.BestTime[GameManager.Instance.numLevel - 1].ToString("F3")}";



        currentTime.text = $"TIME {FindAnyObjectByType<Timer>().CurrentTime.ToString("F3")}";
    }
}
