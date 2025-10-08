using UnityEngine;

namespace Scripts.Services
{
    public class GamepadInputManager : BaseInputManager
    {       
        private GamepadInput.GamepadControlActions _gamepadActions;

        public GamepadInputManager()
        {
            var gameInput = new GamepadInput();
            _gamepadActions = gameInput.GamepadControl;
            gameInput.Enable();
        }

        public override void Update()
        {  
            UpdateLeftStickValue(_gamepadActions.LeftStickMove.ReadValue<Vector2>());          

            UpdateRightStickValue(_gamepadActions.RightStickMove.ReadValue<Vector2>());         
        }
        public override void Disable()
        {
            base.Disable();
            _gamepadActions.Disable();
        }
    }
} 