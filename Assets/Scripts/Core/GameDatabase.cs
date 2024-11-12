using UnityEngine;
using VraiOuFaux.Core.Mascots;
using VraiOuFaux.Core.Questions;

namespace VraiOuFaux.Core
{
    public class GameDatabase
    {
        public QuestionData[] Questions { get; private set; }
        
        public MascotData[] Mascots { get; private set; }
        
        // Permet de récuperer les objet dans les ressources
        public GameDatabase()
        {
            Questions = Resources.LoadAll<QuestionData>("Questions");
            Mascots = Resources.LoadAll<MascotData>("Mascottes");
        }
    }
}