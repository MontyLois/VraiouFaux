using UnityEngine;
using VraiOuFaux.Core.Mascots;
using VraiOuFaux.Core.Questions;

namespace VraiOuFaux.Game
{
    public struct Question
    {
        public MascotData MascotData { get; private set; }
        private readonly QuestionData _data;
        private Animator _animator;

        public Question(QuestionData data, MascotData mascotData)
        {
            _data = data;
            MascotData = mascotData;
            _animator = mascotData.Avatar.GetComponent<Animator>();
        }

        public string GetText()
        {
            return _data.QuestionText;
        }
        
        public bool Answer(bool answer)
        {
            return _data.Solution == answer;
        }
        
        public GameObject GetAvatar()
        {
            return MascotData.Avatar;
        }
    }
}