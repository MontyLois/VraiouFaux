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
        [SerializeField, Range(0, 0.2f)] private float y_offset = 0.08f;
        [SerializeField, Range(0, 0.2f)] private float x_offset = 0.05f;
        
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
        

        public void SetSwipe(bool choice)
        {
            if (choice)
            {
                direction = 1f;
            }
            else
            {
                direction = -1f;
            }
            isSwiped = true;
        }
        
        private void SwipeMascot(float time, float direction)
        {
            //make beautiful swipe curve
            float y = (y_offset * Mathf.Sqrt(time));
            float x = direction*time*x_offset;
            mascotHeadPosition.Translate(x, y, 0);
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
