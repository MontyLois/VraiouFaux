using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using VraiOuFaux.Game;

namespace VraiOuFaux.Core
{
    public class HistoricTextUI: MonoBehaviour
    {
 
        [SerializeField] 
        private LocalizeStringEvent affirmation_localizedString;
        
        [SerializeField] 
        private LocalizeStringEvent solution_localizedString;
        
        [SerializeField] 
        private LocalizeStringEvent explaination_localizedString;

        public void Sync(Question question)
        {
            affirmation_localizedString.StringReference.SetReference(question.GetAffirmation().TableReference,question.GetAffirmation().TableEntryReference);
            affirmation_localizedString.RefreshString();
            
            solution_localizedString.StringReference.SetReference(question.GetSolution().TableReference,question.GetSolution().TableEntryReference);
            solution_localizedString.RefreshString();
            
            explaination_localizedString.StringReference.SetReference(question.GetExplaination().TableReference,question.GetExplaination().TableEntryReference);
            explaination_localizedString.RefreshString();
        }
    }
}