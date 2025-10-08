using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Scripts.Services
{
    public class KeyboardInputManager : BaseInputManager
    {
        private KeyboardInput.KeyboardControlActions _keyboardActions;

        public KeyboardInputManager()
        {
            var gameInput = new KeyboardInput();
            _keyboardActions = gameInput.KeyboardControl;
            gameInput.Enable();
        }

        public override void Update()
        {
            // W/S - газ/тормоз (LeftStickValue.y для движения вперед/назад)
            float motorValue = _keyboardActions.ForwardKey.IsPressed() ? 1f : _keyboardActions.BackwardKey.IsPressed() ? -1f : 0f;
            UpdateLeftStickValue(new Vector2(0f, motorValue));          

            // A/D и стрелки лево/право для поворота (RightStickValue.x для поворота влево/вправо)
            float steeringValue = _keyboardActions.RightKey.IsPressed() ? 1f : _keyboardActions.LeftKey.IsPressed() ? -1f : 0f;
            UpdateRightStickValue(new Vector2(steeringValue, 0f)); 
        }
        public override void Disable()
        {
            base.Disable();
            _keyboardActions.Disable();
        }
    }
} 