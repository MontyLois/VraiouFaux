using TMPro;
using UnityEngine;

namespace VraiOuFaux.Core
{
    public class Text : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI text;
        
        public void Sync(string new_text)
        {
            text.text = new_text;
        }
    }
}
