using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using VraiOuFaux.Core;
using Action = System.Action;
using Screen = UnityEngine.Screen;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


namespace VraiOuFaux.Game
{
    public class Mascot : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Transform mascotHeadPosition;
        [SerializeField]
        private Vector3 initialPosition;
        [SerializeField]
        private Rigidbody mascotRigidBody;
        [SerializeField]
        private SpriteRenderer renderImage;

        private bool isSwiped;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameObject spawner = GameObject.FindWithTag("Spawn");
            if (spawner != null)
            {
                mascotHeadPosition.position = spawner.transform.position;
                mascotHeadPosition.rotation = spawner.transform.rotation;
            }
            initialPosition = mascotHeadPosition.position;
            isSwiped = false;
        }

        
        public void MoveMascot(InputAction.CallbackContext context)
        {
            TouchState touch = context.ReadValue<TouchState>();
            Vector2 position = touch.position;
            Vector2 delta = touch.delta;
            if (Camera.main != null)
            {
                float z = this.mascotHeadPosition.position.z - Camera.main.transform.position.z;
                Vector3 newpos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y,z ));
            
                
            
                Debug.Log("phase : " + touch.phase);
                if (!isSwiped)
                {
                    bool choice = GetTrueOrFalse(position, out bool isMiddle);
                    if ((touch.phase == TouchPhase.Began && IsMouseOverUIWithIgnore(position) )|| touch.phase == TouchPhase.Moved)
                    {
                        mascotHeadPosition.position = new Vector3(newpos.x, newpos.y, mascotHeadPosition.position.z);
                    }
                    switch (choice, isMiddle)
                    {
                        case (true, false):
                            //change color
                            renderImage.color = GameController.GameMetrics.MascotColorGood;
                            if (touch.phase == TouchPhase.Ended)
                            {
                                isSwiped = true;
                                mascotRigidBody.AddForce(200,delta.y * 50,0);
                                StartCoroutine(IAnswerQuestion(true));
                            }
                            break;
                        case (false, false):
                            //change colorS
                            renderImage.color = GameController.GameMetrics.MascotColorBad;
                            Debug.Log("Color RED");
                            if (touch.phase == TouchPhase.Ended)
                            {
                                isSwiped = true;
                                mascotRigidBody.AddForce(-200,delta.y * 50,0);
                                StartCoroutine(IAnswerQuestion(false));
                            }
                            break;
                        default:
                            //reset color
                            renderImage.color = GameController.GameMetrics.MascotColorNormal;
                            if (touch.phase == TouchPhase.Ended)
                            {
                                mascotHeadPosition.position = initialPosition;
                            }
                            break;
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

        /*
         * Answer the question 
         */
        private void AnswerQuestion(bool answer)
        {
            QuestionManager.Instance.AnswerQuestion(answer);
        }
        

        public void DoAnim(Action callback)
        {
            StartCoroutine(IDoAnim(callback));
        }

        private IEnumerator IAnswerQuestion(bool answer)
        {
            yield return new WaitForSeconds(1);
            AnswerQuestion(answer);
        }
        
        private IEnumerator IDoAnim(Action callback)
        {
            yield return new WaitForSeconds(4);
            //yield return new WaitUntil(4);
            callback?.Invoke();
        }

        public void Talk()
        {
            _animator.Play("Mascot_AskQuestion");
        }
       
        public void Wiggle()
        {
            _animator.Play("Mascot_Wiggle");
        }

    }
}
