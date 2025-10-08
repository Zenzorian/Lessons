using UnityEngine;
using System;

namespace Scripts.Services
{
    /// <summary>
    /// Input manager service implementing Strategy pattern for multiple input devices.
    /// Provides unified interface for keyboard and gamepad input with runtime switching capability.
    /// Demonstrates abstraction layer design and polymorphic behavior.
    /// </summary>
    public class InputManagerService : IInputManagerService
    {            
        // Current active input strategy (keyboard or gamepad)
        private IInputManagerService _inputManagerService;
       
        /// <summary>
        /// Left stick/WASD input values normalized to -1 to 1 range.
        /// Y-axis typically used for forward/backward movement.
        /// </summary>
        public Vector2 LeftStickValue => _inputManagerService.LeftStickValue;
        
        /// <summary>
        /// Right stick/arrow keys input values normalized to -1 to 1 range.
        /// X-axis typically used for steering/rotation.
        /// </summary>
        public Vector2 RightStickValue => _inputManagerService.RightStickValue;
        
        /// <summary>
        /// Event triggered when input values change.
        /// Allows reactive programming patterns for input handling.
        /// </summary>
        public event Action OnValueChanged
        {
            add => _inputManagerService.OnValueChanged += value;
            remove => _inputManagerService.OnValueChanged -= value;
        }
       
        /// <summary>
        /// Updates the current input strategy.
        /// Must be called each frame to get latest input values.
        /// </summary>
        public void Update()        
            =>_inputManagerService.Update();        

        /// <summary>
        /// Resets input values to zero state.
        /// Useful for state transitions and input cleanup.
        /// </summary>
        public void Reset()
            =>_inputManagerService.Reset();        

        /// <summary>
        /// Switches input handling to keyboard strategy.
        /// Demonstrates runtime strategy switching with proper cleanup.
        /// </summary>
        public void SwitchToKeyboard()
        {              
           // Clean up current input strategy
           Disable();
           
           // Create new keyboard input strategy
            _inputManagerService = new KeyboardInputManager();  
        }      

        /// <summary>
        /// Switches input handling to gamepad strategy.
        /// Enables controller-based input with analog stick support.
        /// </summary>
        public void SwitchToGamepad()
        { 
            // Clean up current input strategy
            Disable();
            
            // Create new gamepad input strategy
            _inputManagerService = new GamepadInputManager();
        }

        /// <summary>
        /// Disables current input strategy and performs cleanup.
        /// Prevents memory leaks and unwanted input processing.
        /// </summary>
        public void Disable()
            => _inputManagerService?.Disable();
    }
}