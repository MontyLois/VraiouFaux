using System;
using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;
using VraiOuFaux.Core;
using VraiOuFaux.Core.Mascots;
using Random = UnityEngine.Random;

namespace VraiOuFaux.Game
{
    public class QuestionManager : MonoSingleton<QuestionManager>
    {
        public event Action OnComplete;
        public event Action<Question> OnNewQuestion;
        public event Action<Question, bool> OnQuestionAnswered;

        private Queue<Question> questions;
        private GameObject currentMascot;

        [SerializeField] 
        private int questionCount = 11;
        
        protected override void Awake()
        {
            // ask lupeni
            base.Awake();

            /*
             * generate queue of question in random order
             */
            // retreve list of mascots and their question, generate a list with the question
            List<Question> questionsList = new();
            MascotData[] mascots = GameController.GameDatabase.Mascots;
            for (int i = 0; i < mascots.Length; i++)
            {
                MascotData mascot = mascots[i];
                questionsList.Add(new Question(mascot.Question, mascot));
            }

            
            // store question in queue in random order 
            int capacity = Mathf.Min(questionCount, questionsList.Count);
            questions = new Queue<Question>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                //get a question at random index
                int questionIndex = Random.Range(0, questionsList.Count);
                Question question = questionsList[questionIndex];
                //store the question at the end of the queue and delete it from the list
                questions.Enqueue(question);
                questionsList.RemoveAt(questionIndex);
            }
        }

        private void Start()
        {
            SeeNextQuestion();
        }


        public void SeeNextQuestion()
        {
            if (questions.TryPeek(out Question next))
            {
                Debug.Log("New");
                //spawn the mascot
                currentMascot = Instantiate(next.GetAvatar());
                //faire avancer la mascotte et attendre la fin pour trigger l'event
                OnNewQuestion?.Invoke(next);
            }
            else
            {
                OnComplete?.Invoke();
                Debug.Log("Completed");
            }
        }

        /*
         * get result of the answer, trigger OnQuestionAnswerd and diplay next question
         */
        public void AnswerQuestion(bool answer)
        {
            if (questions.TryDequeue(out Question currentQuestion))
            {
                //get the player answer
                bool result = currentQuestion.Answer(answer);
                Debug.Log(result ? "Success" : "Failure");
                OnQuestionAnswered?.Invoke(currentQuestion, result);
                //destroy the mascot
                Destroy(currentMascot);
                //start new question
                SeeNextQuestion();
            }
        }
       
    }
}
