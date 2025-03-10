using UnityEngine;

namespace VraiOuFaux.Core
{
    public class ToggleUI : MonoBehaviour
    {
        public void Toogle()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }
}