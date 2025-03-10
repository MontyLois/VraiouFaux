using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace VraiOuFaux.Core
{
    public class LocalizeTextSync : MonoBehaviour
    {
        public void SyncText(LocalizedString localizedString)
        {
            this.GetComponent<LocalizeStringEvent>().StringReference.SetReference(localizedString.TableReference,
                localizedString.TableEntryReference);
        }
    }
}
