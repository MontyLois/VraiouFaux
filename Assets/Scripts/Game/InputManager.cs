using System;
using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace VraiOuFaux.Game
{
    public class InputManager : MonoSingleton<InputManager>
    {
        //public delegate void OnMoveMascot(TouchState touch);

        public event Action<TouchState> OnMoveMascotEvent;

        public void OnMoveMascot(InputAction.CallbackContext context)
        {
            TouchState touch = context.ReadValue<TouchState>();
            OnMoveMascotEvent?.Invoke(touch);
        }
        
    }
}
