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
             QuestionManager.Instance.OnNewQuestion += ResetSwipe;
         }

         private void OnDisable()
         {
             InputManager.Instance.OnTouchEvent -= MoveMascot;
             QuestionManager.Instance.OnNewQuestion -= ResetSwipe;
         }

         public void MoveMascot(TouchState touch)
         {
                    Vector2 position = touch.position;
                    Vector2 delta = touch.delta;
                    if (Camera.main != null)
                    {
                        if (!isSwiped)
                        {
                            if (!IsMouseOverUIWithIgnore(position))
                            {
                                QuestionManager.Instance.ResetCurrentMascot();
                            }
                            else
                            {
                                if (touch.phase == TouchPhase.Began|| touch.phase == TouchPhase.Moved)
                                {
                                    if (IsMouseOverUIWithIgnore(position))
                                    {
                                        QuestionManager.Instance.MoveCurrentMascot(position);
                                    }
                                }

                                if (touch.phase == TouchPhase.Ended)
                                {
                                    bool choice = GetTrueOrFalse(position, out bool isMiddle);
                                    Debug.Log("We are throwing");
                                    if (isMiddle)
                                    {
                                        QuestionManager.Instance.ResetCurrentMascot();
                                    }
                                    else
                                    {
                                        isSwiped = true;
                                        QuestionManager.Instance.ThrowMascot(choice, position);
                                    }
                                }
                            }
                        }
                    }
         }

         public void ResetSwipe(Question q)
         {
             isSwiped = false;
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
