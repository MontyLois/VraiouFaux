using UnityEngine;

namespace VraiOuFaux.Core.Questions
{
    [CreateAssetMenu(menuName = "VraiOuFaux/Question")]
    public class QuestionData : ScriptableObject
    {
        [field: SerializeField]
        public string QuestionText { get; private set; }
        [field: SerializeField]
        public bool Solution { get; private set; }
        [field: SerializeField]
        public string Explanation { get; private set; }
        [field: SerializeField]
        public bool PlayerAnswer { get; set; }
        

        /*
         
         Pareil qu'au dessus :
         
        [SerializeField]
        private string _questionText;

        public string QQuestionText
        {
            get
            {
                Debug.Log("1");
                return _questionText;
            }
            set
            {
                _questionText = value;
            }
        }
        */
    }
}
