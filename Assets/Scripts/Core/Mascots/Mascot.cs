using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using Action = System.Action;


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
        private SpriteRenderer renderImage;

        private bool isSwiped;
        private float time = 0;
        private float direction = 1;
        private float xposition =0;
        private float yposition =0;   
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mascotHeadPosition.position = this.transform.position; 
            mascotHeadPosition.rotation = this.transform.rotation;
            
            initialPosition = mascotHeadPosition.position;
            isSwiped = false;
            
            transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x,0f,0f);
        }

        private void FixedUpdate()
        {
            if (isSwiped)
            {
                time ++;
                SwipeMascot(time,direction);
            }
        }

        public void GetDragged(Vector2 position)
        {
            if (Camera.main != null)
            {
                float z = this.mascotHeadPosition.position.z - Camera.main.transform.position.z;
                Vector3 newpos = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y,z ));
                mascotHeadPosition.position = new Vector3(newpos.x, newpos.y, mascotHeadPosition.position.z);
            }
        }
        
        public void MoveToPosition(Vector3 position)
        {
            mascotHeadPosition.SetLocalPositionAndRotation(position, mascotHeadPosition.rotation);
        }

        public void ResetPosition()
        {
            mascotHeadPosition.position = initialPosition;
        }
        

        public void Swipe(bool choice)
        {
            if (choice)
            {
                direction = -1f;
            }
            else
            {
                direction = 1f;
            }

            Debug.Log("x position  "+ mascotHeadPosition.position.x+" y position : "+mascotHeadPosition.position.y);
            xposition = mascotHeadPosition.localPosition.x;
            yposition = mascotHeadPosition.localPosition.y;
            
            isSwiped = true;
        }
        private void SwipeMascot(float time, float direction)
        {
            float y = 0.1f * Mathf.Sqrt(time-xposition)+yposition;
            float x = direction*time*0.1f + xposition;
            Vector3 position = new Vector3(x, y, 0);
            mascotHeadPosition.SetLocalPositionAndRotation(position, mascotHeadPosition.localRotation);
        }
        
        

        public void DoAnim(Action callback)
        {
            StartCoroutine(IDoAnim(callback));
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
