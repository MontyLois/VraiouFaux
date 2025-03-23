using System;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow.CodeReview.ReviewChanges.Summary;
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
    
    //For Question Info 
    [field: SerializeField] private GameObject mascot_UI;
    [field: SerializeField] private LocalizeTextSync question_Title_Text;
    [field: SerializeField] private LocalizeTextSync question_Solution_Text;
    [field: SerializeField] private LocalizeTextSync question_Explaination_Text;
    
    //For animal info
    [field: SerializeField] private GameObject animal_Info_UI;
    [field: SerializeField] private LocalizeTextSync animal_Name_Text;
    [field: SerializeField] private LocalizeTextSync animal_Info_Text;
    
    //For Finish
    [field: SerializeField] private GameObject finish_UI;
    
    //For wrong or right
    [field: SerializeField] private GameObject wrongAnswers;
    [field: SerializeField] private GameObject wrongAnswersUI;
    [field: SerializeField] private GameObject rightAnswers;
    [field: SerializeField] private GameObject rightAnswersUI;
    
    [field: SerializeField]
    private Dictionary<GameObject, GameObject> uiToClose;

    public void OnEnable()
    {
        HistoricManager.Instance.OnMascotSelected += MascotSelectedUI;
        HistoricManager.Instance.OnMascotUnselected += CloseMascotSelectedUI;
        HistoricManager.Instance.OnInfoAnimalOpen += AnimalOpenInfo;
        selected_Mascot_UI.SetActive(false);
    }

    public void OnDisable()
    {
        HistoricManager.Instance.OnMascotSelected -= MascotSelectedUI;
        HistoricManager.Instance.OnMascotUnselected -= CloseMascotSelectedUI;
        HistoricManager.Instance.OnInfoAnimalOpen -= AnimalOpenInfo;
    }

    private void MascotSelectedUI(Question question)
    {
        //historicTextUITextUI.Sync(question);
        QuestionUISync(question);
        ToggleQuestionUI();
       // selected_Mascot_UI.SetActive(true);
    }

    private void AnimalOpenInfo(Question question)
    {
        AnimalUISync(question);
        ToggleAnimalUI();
    }

    private void CloseMascotSelectedUI()
    {
        selected_Mascot_UI.SetActive(false);
    }

    private void QuestionUISync(Question question)
    {
        question_Title_Text.SyncText(question._data.Question_Key_Text);
        question_Solution_Text.SyncText(question._data.Solution_Key_Text);
        question_Explaination_Text.SyncText(question._data.Explaination_Key_Text);
    }
    
    private void AnimalUISync(Question question)
    {
        animal_Name_Text.SyncText(question.MascotData.Animal_Name_Key_Text);
        animal_Info_Text.SyncText(question.MascotData.Animal_Info_Key_Text);
    }
    
    public void ToggleQuestionUI()
    {
        mascot_UI.SetActive(!mascot_UI.activeSelf);
    }
    
    public void ToggleAnimalUI()
    {
        animal_Info_UI.SetActive(!animal_Info_UI.activeSelf);
    }
    
    public void ToggleFinish()
    {
        finish_UI.SetActive(!mascot_UI.activeSelf);
    }

    public void SelectRighAnswers()
    {
        wrongAnswers.SetActive(false);
        wrongAnswersUI.SetActive(false);
        rightAnswers.SetActive(true);
        rightAnswersUI.SetActive(true);
    }
    
    public void SelectWrongAnswers()
    {
        rightAnswers.SetActive(false);
        rightAnswersUI.SetActive(false);
        wrongAnswers.SetActive(true);
        wrongAnswersUI.SetActive(true);
    }
}
