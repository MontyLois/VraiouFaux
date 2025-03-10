using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace VraiOuFaux.Game.UI
{
    public class QuestionTextUI : MonoBehaviour
    {
       
        [SerializeField] 
        private LocalizeStringEvent localizedStringEvent;
        
        
        public void Sync(Question question)
        {
            localizedStringEvent.StringReference.SetReference(question.GetAffirmation().TableReference,question.GetAffirmation().TableEntryReference);
            localizedStringEvent.RefreshString();
        }
    }
}