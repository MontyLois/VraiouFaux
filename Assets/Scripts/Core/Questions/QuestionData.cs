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
    }
}
