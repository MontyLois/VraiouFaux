using UnityEngine;

namespace VraiOuFaux.Core
{
    public class MenueManagerUI : MonoBehaviour
    {
        [field: SerializeField]
        private GameObject menue_Pause_UI;
        
        [field: SerializeField]
        private GameObject button_Pause_UI;
        
        [field: SerializeField]
        private GameObject screen_InputBlcoker_UI;
        
        [field: SerializeField]
        private GameObject menu_setting_UI;

        public void ToggleUI()
        {
            menue_Pause_UI.SetActive(!menue_Pause_UI.activeSelf);
            button_Pause_UI.SetActive(!button_Pause_UI.activeSelf);
            screen_InputBlcoker_UI.SetActive(!screen_InputBlcoker_UI.activeSelf);
        }

        public void OpenSettings()
        {
            menu_setting_UI.SetActive(true);
        }
    }
}
