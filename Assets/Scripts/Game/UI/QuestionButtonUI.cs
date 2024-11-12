using UnityEngine;

namespace VraiOuFaux.Game.UI
{
    public class QuestionButtonUI : MonoBehaviour
    {
        [SerializeField] 
        private bool isTrue;

        [SerializeField] 
        private QuestionManagerUI managerUI;
        
        public void Answer()
        {
            managerUI.Answer(isTrue);
        }
    }
}