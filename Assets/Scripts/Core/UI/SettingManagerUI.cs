using UnityEngine;

namespace VraiOuFaux.Core
{
    public class SettingManagerUI : MonoBehaviour
    {
        [field: SerializeField]
        private GameObject menu_setting_UI;
        
        
        public void CloseSettings()
        {
            menu_setting_UI.SetActive(false);
        }
    }
}
