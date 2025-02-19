using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace VraiOuFaux.Game.UI
{
    public class QuestionTextUI : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI text;

        public void Sync(Question question)
        {
            var localizedString = question.GetAffirmation().GetLocalizedString();
            text.text = localizedString;
        }
    }
}