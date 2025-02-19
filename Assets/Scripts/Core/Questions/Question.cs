using UnityEngine;
using UnityEngine.Localization;
using VraiOuFaux.Core.Mascots;
using VraiOuFaux.Core.Questions;

namespace VraiOuFaux.Game
{
    public struct Question
    {
        public MascotData MascotData { get; private set; }
        public QuestionData _data { get; private set; }

        public Question(QuestionData data, MascotData mascotData)
        {
            _data = data;
            MascotData = mascotData;
        }

        public LocalizedString GetAffirmation()
        {
            return _data.Question_Key_Text;
        }
        
        public LocalizedString GetSolution()
        {
            return _data.Solution_Key_Text;
        }
        
        public LocalizedString GetExplaination()
        {
            return _data.Explaination_Key_Text;
        }
        
        public bool Answer(bool answer)
        {
            return _data.SolutionB == answer;
        }
        
        public GameObject GetAvatar()
        {
            return MascotData.Avatar;
        }
    }
}