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
         private bool canInteract=true;

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
                    if (Camera.main != null)
                    {
                        if (!isSwiped)
                        {
                            if (!IsMouseOverUIWithIgnore(position))
                            {
                                QuestionManager.Instance.ResetCurrentMascot();
                                canInteract = false;
                            }
                            else
                            {
                                if (touch.phase == TouchPhase.Began)
                                {
                                    canInteract = true;
                                }

                                if (canInteract)
                                {
                                    switch (touch.phase)
                                    {
                                        case TouchPhase.Began : QuestionManager.Instance.MoveCurrentMascot(position);
                                            break;
                                        case TouchPhase.Moved : QuestionManager.Instance.MoveCurrentMascot(position);
                                            break;
                                        case TouchPhase.Ended :
                                            bool choice = GetTrueOrFalse(position, out bool isMiddle);
                                            if (isMiddle)
                                            {
                                                QuestionManager.Instance.ResetCurrentMascot();
                                            }
                                            else
                                            {
                                                isSwiped = true;
                                                QuestionManager.Instance.ThrowMascot(choice, position);
                                            }
                                            break;
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
