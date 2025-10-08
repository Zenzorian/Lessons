using UnityEngine;

namespace Scripts.Logic
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
                
        private const float SMOOTH_SPEED = 5f;
        private const float ROTATION_SMOOTH_SPEED = 5f;
        private static readonly Vector3 CAMERA_OFFSET = new Vector3(0, 5, -10);
        private const float CAMERA_ANGLE = 15f; // Угол наклона камеры вниз

        private void LateUpdate()
        {
            if (target == null) return;
           
            // Получаем только Y-поворот цели (горизонтальное вращение)
            float targetYRotation = target.eulerAngles.y;
            
            // Вычисляем желаемую позицию с учетом только Y-поворота
            Quaternion yOnlyRotation = Quaternion.Euler(0, targetYRotation, 0);
            Vector3 desiredPosition = target.position + yOnlyRotation * CAMERA_OFFSET;
            
            // Плавно перемещаем камеру к желаемой позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SMOOTH_SPEED * Time.deltaTime);
            transform.position = smoothedPosition;
           
            // Создаем желаемое вращение: Y от цели + фиксированный угол наклона по X
            Quaternion desiredRotation = Quaternion.Euler(CAMERA_ANGLE, targetYRotation, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, ROTATION_SMOOTH_SPEED * Time.deltaTime);
        }
    }
} 