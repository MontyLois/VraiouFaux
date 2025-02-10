using System.Collections;
using UnityEngine;
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
        private Rigidbody mascotRigidBody;
        [SerializeField]
        private SpriteRenderer renderImage;

        private bool isSwiped;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mascotHeadPosition.position = this.transform.position; 
            mascotHeadPosition.rotation = this.transform.rotation;
            
            initialPosition = mascotHeadPosition.position;
            isSwiped = false;
            
            transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x,0f,0f);
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

        public void ThrowMascot(bool choice, Vector2 delta)
        {
            Vector2 force = new Vector2(200*Mathf.Sign(delta.x), delta.y * 50);
            if (choice)
            {
                mascotRigidBody.AddForce(force);
            }
            else
            {
                mascotRigidBody.AddForce(force);
            }
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
