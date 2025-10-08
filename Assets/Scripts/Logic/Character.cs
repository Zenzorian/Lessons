using Scripts.ScriptableObjects;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Logic
{
    public class Character : MonoBehaviour
    {
        [Header("Wheel Colliders")]
        [Tooltip("Physics colliders for wheel simulation")]
        public WheelCollider frontLeftWheel;
        public WheelCollider frontRightWheel;
        public WheelCollider rearLeftWheel;
        public WheelCollider rearRightWheel;
        
        [Header("Wheel Visual Objects")]
        [Tooltip("Visual meshes synchronized with wheel collider positions")]
        public Transform frontLeftWheelMesh;
        public Transform frontRightWheelMesh;
        public Transform rearLeftWheelMesh;
        public Transform rearRightWheelMesh;

       
        private CharacterConfig _characterConfig;      
       
       
        private IInputManagerService _inputManagerService;

       
        private Rigidbody _rigidbody;

      
        public void Construct(IInputManagerService inputManagerService, CharacterConfig characterConfig)
        {
           _inputManagerService = inputManagerService;
           _characterConfig= characterConfig;
         
           _rigidbody = gameObject.GetComponent<Rigidbody>();
        }
       
        private void Update()
        {   
            if(_inputManagerService == null||_characterConfig == null)return;
            
            _inputManagerService.Update();
            
            float motor = _inputManagerService.LeftStickValue.y;
            float steering = _inputManagerService.RightStickValue.x;
            
            HandleMotor(motor);
            HandleSteering(steering);
          
            UpdateWheelVisuals();
        }
        
        private void HandleMotor(float motorInput)
        {
            float motorTorque = motorInput * _characterConfig.maxMotorTorque;
          
            frontLeftWheel.motorTorque = motorTorque;
            frontRightWheel.motorTorque = motorTorque;
            rearLeftWheel.motorTorque = motorTorque;
            rearRightWheel.motorTorque = motorTorque;
            
            // Automatic braking system when no input is detected
            if (Mathf.Abs(motorInput) < 0.1f)
            {
                frontLeftWheel.brakeTorque = _characterConfig.brakeTorque;
                frontRightWheel.brakeTorque = _characterConfig.brakeTorque;
                rearLeftWheel.brakeTorque = _characterConfig.brakeTorque;
                rearRightWheel.brakeTorque = _characterConfig.brakeTorque;
            }
            else
            {
                frontLeftWheel.brakeTorque = 0;
                frontRightWheel.brakeTorque = 0;
                rearLeftWheel.brakeTorque = 0;
                rearRightWheel.brakeTorque = 0;
            }
        }
        
        private void HandleSteering(float steeringInput)
        {
            float steerAngle = steeringInput * _characterConfig.maxSteerAngle;
           
            frontLeftWheel.steerAngle = steerAngle;
            frontRightWheel.steerAngle = steerAngle;
        }
        
       
        private void UpdateWheelVisuals()
        {
            UpdateWheelVisual(frontLeftWheel, frontLeftWheelMesh);
            UpdateWheelVisual(frontRightWheel, frontRightWheelMesh);
            UpdateWheelVisual(rearLeftWheel, rearLeftWheelMesh);
            UpdateWheelVisual(rearRightWheel, rearRightWheelMesh);
        }
        
        private void UpdateWheelVisual(WheelCollider wheelCollider, Transform wheelMesh)
        {
            // Null safety checks for robustness
            if (wheelCollider == null || wheelMesh == null) return;
            
            Vector3 position;
            Quaternion rotation;
            
            // Get world position and rotation from physics simulation
            wheelCollider.GetWorldPose(out position, out rotation);
            
            // Apply physics data to visual representation
            wheelMesh.position = position;
            wheelMesh.rotation = rotation;
        }       
    }
}