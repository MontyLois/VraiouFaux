using UnityEngine;
using VraiOuFaux.Core.Questions;

namespace VraiOuFaux.Core.Mascots
{
    [CreateAssetMenu(fileName = "Mascot", menuName = "VraiOuFaux/Mascot")]
    public class MascotData : ScriptableObject
    {
        [field: SerializeField]
        public QuestionData Question { get; private set; }
        
        [field: SerializeField]
        public GameObject Avatar { get; private set; }
    }
}
