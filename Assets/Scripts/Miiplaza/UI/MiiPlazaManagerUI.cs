using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VraiOuFaux.Core.Mascots;
using VraiOuFaux.Game;

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
   
    [field: SerializeField]
    private Dictionary<GameObject, GameObject> uiToClose;

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

    private void MascotSelectedUI(Question question)
    {
        questionText.text = question.GetAffirmation().GetLocalizedString();
        questionSolution.text = question.GetSolution().GetLocalizedString();
        questionExplaination.text = question.GetExplaination().GetLocalizedString();
        selected_Mascot_UI.SetActive(true);
    }

    private void CloseMascotSelectedUI()
    {
        selected_Mascot_UI.SetActive(false);
    }
    
}
