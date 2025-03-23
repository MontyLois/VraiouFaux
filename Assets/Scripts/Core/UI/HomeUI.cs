using UnityEngine;
using UnityEngine.SceneManagement;

namespace VraiOuFaux.Core
{
    public class HomeUI : MonoBehaviour
    {

        public void LoadMainGame()
        {
            GameManager.Instance.ResetAnswers();
            SceneManager.LoadScene("QuestionsTests");
        }
        
        public void LoadHomeScreen()
        {
            GameManager.Instance.ResetAnswers();
            SceneManager.LoadScene("Home");
        }
    }
}
