using System;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace VraiOuFaux.Game
{
    public class InputManager : MonoSingleton<InputManager>
    {
        private bool isSwiped;
        public event Action<TouchState> OnTouchEvent;
        public void OnTouch(InputAction.CallbackContext context)
        {
            TouchState touch = context.ReadValue<TouchState>();
            OnTouchEvent?.Invoke(touch);
        }
    }
}
