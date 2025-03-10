using UnityEngine;
using UnityEngine.Localization;
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
        
        [field: SerializeField]
        public LocalizedString Animal_Name_Key_Text { get; private set; }
        
        [field: SerializeField]
        public LocalizedString Animal_Info_Key_Text { get; private set; }
       
    }
}
