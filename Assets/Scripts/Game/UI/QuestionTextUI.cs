using TMPro;
using UnityEngine;

namespace VraiOuFaux.Game.UI
{
    public class QuestionTextUI : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI text;

        public void Sync(Question question)
        {
            text.text = question.GetText();
        }
    }
}