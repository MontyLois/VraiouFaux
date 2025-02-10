using UnityEngine;
using VraiOuFaux.Game;

public class HistoricManager : MonoBehaviour
{

    private GameObject selectedMascot;
    [field: SerializeField]
    private Transform selectionTransform;

    private void SelectMascot(GameObject mascot)
    {
        if (selectedMascot)
        {
            ResetCurrentMascot();
        }
        selectedMascot = mascot;
        if (mascot)
        {
            MoveCurrentMascot(selectionTransform.position);
            //on mets l'ui à jour
        }
        else
        {
            // on ferme l'ui
        }
    }
    
    public void MoveCurrentMascot(Vector2 position)
    {
        selectedMascot.GetComponent<Mascot>().MoveToPosition(position);
    }

    public void ResetCurrentMascot()
    {
        selectedMascot.GetComponent<Mascot>().ResetPosition();
    }
}
