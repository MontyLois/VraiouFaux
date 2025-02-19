using UnityEngine;
using UnityEngine.EventSystems;

namespace VraiOuFaux.Core
{
    public class ToggleUI : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            HistoricManager.Instance.ResetCurrentMascot();
        }
    }
}
