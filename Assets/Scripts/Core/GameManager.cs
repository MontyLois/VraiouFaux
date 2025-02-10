using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;
using VraiOuFaux.Game;

namespace VraiOuFaux.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public  List<(Question, bool)> playerAnswers { get; private set; }

        private void Start()
        {
            playerAnswers = new List<(Question, bool)>();
        }

        public void AddAnswer((Question, bool) answer)
        {
            playerAnswers.Add(answer);
        }
        
        
    }
}
