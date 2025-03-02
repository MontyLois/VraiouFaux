using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using VraiOuFaux.Core;
using VraiOuFaux.Core.Mascots;
using VraiOuFaux.Game;

public class MiiPlazaUIManager : MonoBehaviour
{
    [field: SerializeField]
    private GameObject selected_Mascot_UI;
    [SerializeField]
    private HistoricTextUI historicTextUITextUI;
   
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
        historicTextUITextUI.Sync(question);
        selected_Mascot_UI.SetActive(true);
    }

    private void CloseMascotSelectedUI()
    {
        selected_Mascot_UI.SetActive(false);
    }
    
}
