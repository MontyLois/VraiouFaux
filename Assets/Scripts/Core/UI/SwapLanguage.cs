using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace VraiOuFaux.Core
{
    public class SwapLanguage : MonoBehaviour
    {

        private bool active;
        
        private void Start()
        {
            int savedIndex = PlayerPrefs.GetInt("SelectedLanguageIndex", 0);
            ChangeLocal(savedIndex);
        }
        
        public void ChangeLocal(int id)
        {
            Debug.Log(id);
            if (active)
            {
                return;
            }
            StartCoroutine(SetLocal(id));
        }
        
        IEnumerator SetLocal(int localID)
        {
            active = true;
            if (!LocalizationSettings.InitializationOperation.IsDone)
            {
                yield return LocalizationSettings.InitializationOperation;
            }
            if (localID >= 0 && localID < LocalizationSettings.AvailableLocales.Locales.Count)
            {
                // Set language by index
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localID];

                // Save choice for future sessions
                PlayerPrefs.SetInt("SelectedLanguageIndex", localID);
                PlayerPrefs.Save();

                Debug.Log("Language changed to: " + LocalizationSettings.SelectedLocale.Identifier.Code);
            }
            else
            {
                Debug.LogWarning("Invalid language index: " + localID);
            }
            active = false;
        }
    }
}
