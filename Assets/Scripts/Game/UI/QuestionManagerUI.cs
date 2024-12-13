using UnityEngine;

namespace VraiOuFaux.Game.UI
{
    public class QuestionManagerUI : MonoBehaviour
    {
        [SerializeField]
        private QuestionTextUI questionTextUI;
        [SerializeField]
        private GameObject dialogue_box;
        
        /*
         * subscribe to questionManagers events
         */
        private void OnEnable()
        {
            QuestionManager.Instance.OnQuestionAnswered += OnQuestionAnswered;
            QuestionManager.Instance.OnNewQuestion += OnNewQuestion;
            dialogue_box.SetActive(false);
        }

        private void OnDisable()
        {
            
            QuestionManager.Instance.OnQuestionAnswered -= OnQuestionAnswered;
            QuestionManager.Instance.OnNewQuestion -= OnNewQuestion;
        }

        /*
         * return the answer choose by the player
         */
        public void Answer(bool value)
        {
            dialogue_box.SetActive(false);
            QuestionManager.Instance.AnswerQuestion(value);
        }
        
        /*
         * Display new question
         */
        private void OnNewQuestion(Question question)
        {
            dialogue_box.SetActive(true);
            questionTextUI.Sync(question);
        }

        private void OnQuestionAnswered(Question question, bool result)
        {
            dialogue_box.SetActive(false);
        }

    }
}
