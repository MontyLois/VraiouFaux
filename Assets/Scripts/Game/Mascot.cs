using System;
using System.Collections;
using PlasticGui.Configuration.CloudEdition.Welcome;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VraiOuFaux.Game
{
    public class Mascot : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
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
