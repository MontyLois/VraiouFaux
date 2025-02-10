using System;
using TMPro;
using UnityEngine;
using VraiOuFaux.Core.Mascots;

public class MiiPlazaUIManager : MonoBehaviour
{
    [field: SerializeField]
    private GameObject selected_Mascot_UI;
    [field: SerializeField]
    private TextMeshProUGUI questionText;
    [field: SerializeField]
    private TextMeshProUGUI questionSolution;
    [field: SerializeField]
    private TextMeshProUGUI questionExplaination;

    public void OnEnable()
    {
        HistoricManager.Instance.OnMascotSelected += MascotSelectedUI;
        HistoricManager.Instance.OnMascotUnselected += CloseMascotSelectedUI;
        selected_Mascot_UI.SetActive(false);
    }

    public void OnDisable()
    {
        HistoricManager.Instance.OnMascotSelected -= MascotSelectedUI;
        HistoricManager.Instance.OnMascotUnselected -= CloseMascotSelectedUI;
    }

    private void MascotSelectedUI(MascotData mascotdata)
    {
        selected_Mascot_UI.SetActive(true);
        questionText.text = mascotdata.Question.QuestionText;
        questionSolution.text = mascotdata.Question.SolutionText;
        questionExplaination.text = mascotdata.Question.Explanation;
    }

    private void CloseMascotSelectedUI()
    {
        selected_Mascot_UI.SetActive(false);
    }
}
