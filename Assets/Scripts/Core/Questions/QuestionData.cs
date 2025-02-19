using UnityEngine;
using UnityEngine.Localization;

namespace VraiOuFaux.Core.Questions
{
    [CreateAssetMenu(menuName = "VraiOuFaux/Question")]
    public class QuestionData : ScriptableObject
    {
        [field: SerializeField]
        public string QuestionID { get; private set; }
        [field: SerializeField]
        public string QuestionText { get; private set; }
        [field: SerializeField]
        public bool SolutionB { get; private set; }
        [field: SerializeField]
        public string SolutionText { get; private set; }
        [field: SerializeField]
        public string Explanation { get; private set; }
        [field: SerializeField]
        public bool PlayerAnswer { get; set; }
        
        [field: SerializeField]
        public LocalizedString Question_Key_Text { get; private set; }
        
        [field: SerializeField]
        public LocalizedString Solution_Key_Text { get; private set; }
        
        [field: SerializeField]
        public LocalizedString Explaination_Key_Text { get; private set; }
    }
}
