using UnityEngine;
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

        public string GetText()
        {
            return _data.QuestionText;
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