using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [System.Serializable, CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        public float maxMotorTorque = 500f;       
        public float brakeTorque = 1000f;   
        public float maxSteerAngle = 30f; // Максимальный угол поворота передних колес
    }
}