using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using VraiOuFaux.Game;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class MascotPlazzaMovement : MonoBehaviour
{private void OnEnable()
    {
        InputManager.Instance.OnTouchEvent += MoveMascot;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchEvent -= MoveMascot;
    }

    
    public void MoveMascot(TouchState touch)
    {
        if(touch.phase == TouchPhase.Ended)
        {
            GameObject mascot = IsMouseOverMascot(touch.position);
            if (mascot)
            {
                HistoricManager.Instance.SelectMascot(mascot);
            }
        }
    }
    
    /*
     * Check if user touch a mascot
     */
    private GameObject IsMouseOverMascot(Vector2 touchPosition)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("what are we touching ? "+ hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1);
            if (hit.collider.CompareTag("Mascot"))
            {
                Debug.Log("we are touching mascot");
                return hit.collider.gameObject;
            }
        }

        
        
        return null;
    }

}
