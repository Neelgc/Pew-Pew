using System.Collections;
using TMPro;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI feedbackText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SaveButtonAction()
    {
        SavingLocal.Instance.SaveLocalData();
        StartCoroutine(FeedbackText());
    }

    public IEnumerator FeedbackText()
    {
        feedbackText.text = "DATA SAVED";
        yield return new WaitForSeconds(2.5f);
        feedbackText.text = "";
    }
}
