using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace VraiOuFaux.Game
{
    public class MascotQuestionMovement : MonoBehaviour
    {
         private bool isSwiped;

         private void OnEnable()
         {
             InputManager.Instance.OnTouchEvent += MoveMascot;
         }

         private void OnDisable()
         {
             InputManager.Instance.OnTouchEvent -= MoveMascot;
         }

         public void MoveMascot(TouchState touch)
         {
                    Vector2 position = touch.position;
                    Vector2 delta = touch.delta;
                    if (Camera.main != null)
                    {
                        Debug.Log("phase : " + touch.phase);
                        if (!isSwiped)
                        {
                            bool choice = GetTrueOrFalse(position, out bool isMiddle);
                            if ((touch.phase == TouchPhase.Began && IsMouseOverUIWithIgnore(position) )|| touch.phase == TouchPhase.Moved)
                            {
                                QuestionManager.Instance.MoveCurrentMascot(position);
                            }
        
                            if (touch.phase == TouchPhase.Ended)
                            {
                                if (isMiddle)
                                {
                                    QuestionManager.Instance.ResetCurrentMascot();
                                }
                                else
                                {
                                    QuestionManager.Instance.ThrowMascot(choice, delta);
                                }
                            }
                        }
                    }
         }
        
                /*
                 * Use GameObject with tag to determine if finger position is on the true or false side
                 */
                private bool GetTrueOrFalse(Vector2 touchPosition, out bool isMiddle)
                {
                    isMiddle = false;
                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    pointerEventData.position = touchPosition;
        
                    List<RaycastResult> raycastResultsList = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
                    for (int i = 0; i < raycastResultsList.Count; i++)
                    {
                        if (raycastResultsList[i].gameObject.CompareTag("False"))
                        {
                            return false;
                        }
                        else if (raycastResultsList[i].gameObject.CompareTag("True"))
                        {
                            return true;
                        }
                    }
                    isMiddle = true;
                    return default;
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
}
