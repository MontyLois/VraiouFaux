using UnityEngine;
using UnityEngine.SceneManagement;

namespace VraiOuFaux.Core
{
    public class HomeUI : MonoBehaviour
    {

        public void LoadMainGame()
        {
            SceneManager.LoadScene("QuestionsTests");
        }
        
        public void LoadHomeScreen()
        {
            SceneManager.LoadScene("Home");
        }
    }
}
