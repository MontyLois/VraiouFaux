using System;
using System.Collections;
using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VraiOuFaux.Core;
using VraiOuFaux.Core.Mascots;
using Random = UnityEngine.Random;

namespace VraiOuFaux.Game
{
    public class QuestionManager : MonoSingleton<QuestionManager>
    {
        public event Action<List<(Question, bool)>> OnComplete;
        public event Action<Question> OnNewQuestion;
        public event Action<Question, bool> OnQuestionAnswered;

        private Queue<Question> questions;
        private List<(Question, bool)> playerAnswers;
        //the mascot and its spawn
        private GameObject currentMascot;
        private Transform spawnTransform;
        

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
            Debug.Log(mascots.Length);
            
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
            playerAnswers = new List<(Question, bool)>();
            GameObject spawn = GameObject.FindWithTag("Spawn");
            if (spawn)
            {
                spawnTransform = spawn.GetComponent<Transform>();
            }
            SeeNextQuestion();
        }


        public void SeeNextQuestion()
        {
            Debug.Log("question restante : "+ questions.Count);
            //check if there's still question left
            if (questions.TryPeek(out Question next))
            {
                Debug.Log("New");
                //spawn the mascot
                currentMascot = Instantiate(next.GetAvatar(), spawnTransform);
                Debug.Log(" wa instantiate "+ questions.Count);
                OnNewQuestion?.Invoke(next);
            }
            else
            {
                //no more question, the quizz is completed
                OnComplete?.Invoke(playerAnswers);
                Debug.Log("Completed");
                SceneManager.LoadScene("HistoricTests");
            }
        }

        /*
         * get result of the answer, trigger OnQuestionAnswerd and diplay next question
         */
        public void AnswerQuestion(bool answer)
        {
            if (questions.TryDequeue(out Question currentQuestion))
            {
                //get the player answer and invok
                bool result = currentQuestion.Answer(answer);
                Debug.Log(result ? "Success" : "Failure");
                OnQuestionAnswered?.Invoke(currentQuestion, result);
                currentQuestion._data.PlayerAnswer = answer;
                playerAnswers.Add((currentQuestion,answer));
                GameManager.Instance.AddAnswer((currentQuestion,answer));
                //destroy the mascot
                Destroy(currentMascot);
                //start new question
                SeeNextQuestion();
            }
        }

        public void MoveCurrentMascot(Vector2 position)
        {
            currentMascot.GetComponent<Mascot>().GetDragged(position);
        }
        public void ResetCurrentMascot()
        {
            currentMascot.GetComponent<Mascot>().ResetPosition();
        }

        public void ThrowMascot(bool choice, Vector2 delta)
        {
            currentMascot.GetComponent<Mascot>().ThrowMascot(choice, delta);
            StartCoroutine(IAnswerQuestion(choice));
            
        }
        
        private IEnumerator IAnswerQuestion(bool choice)
        {
            yield return new WaitForSeconds(1.5f);
            AnswerQuestion(choice);
        }
    }
}
