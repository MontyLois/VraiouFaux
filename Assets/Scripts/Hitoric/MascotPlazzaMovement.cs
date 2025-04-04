using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using VraiOuFaux.Game;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class MascotPlazzaMovement : MonoBehaviour
{
    private bool canSelect = true;
    private GameObject mascot = null;
    
    private void OnEnable()
    {
        InputManager.Instance.OnTouchEvent += MoveMascot;
        HistoricManager.Instance.OnMascotUnselected += ResetSelectable;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchEvent -= MoveMascot;
        HistoricManager.Instance.OnMascotUnselected -= ResetSelectable;
    }

    
    public void MoveMascot(TouchState touch)
    {
        if (IsMouseOverUIWithIgnore(touch.position))
        {
            if (!mascot && touch.phase == TouchPhase.Began)
            {
                canSelect = true;
            }
            if(touch.phase == TouchPhase.Ended && canSelect)
            {
                GameObject mascot = IsMouseOverMascot(touch.position);
                if (mascot)
                {
                    HistoricManager.Instance.SelectMascot(mascot);
                    canSelect = false;
                }
            }
        }
    }
    
    /*
     * Check if user touch a mascot
     */
    private GameObject IsMouseOverMascot(Vector2 touchPosition)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        
        
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("what are we touching ? "+ hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1);
            if (hit.collider.CompareTag("Mascot"))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    private void ResetSelectable()
    {
        mascot = null;
        //canSelect = true;
    }

    /*
     * Check if user touch UI and thus should not move the mascot
     */
    private bool IsMouseOverUIWithIgnore(Vector2 touchPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = touchPosition;
        
        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.CompareTag("NotGamePanel"))
            {
                return false;
            }
        }
        return true;
    }
    
}
