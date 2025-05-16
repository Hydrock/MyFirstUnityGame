using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;         // Игрок
    public Vector3 offset = new Vector3(0, 2f, -5f); // Расстояние до игрока
    public float sensitivity = 2f;   // Чувствительность мышки
    public float distance = 5f;      // Дистанция до игрока
    public float minY = -20f;        // Минимальный угол наклона
    public float maxY = 60f;         // Максимальный угол наклона

    private float currentYaw = 0f;   // Горизонтальный угол
    private float currentPitch = 20f; // Вертикальный угол (наклон)

    void LateUpdate()
    {
        // Получаем ввод мышки
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minY, maxY);

        // Вычисляем позицию камеры вокруг игрока
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 direction = rotation * Vector3.back * distance;

        transform.position = target.position + offset + direction;
        transform.LookAt(target.position + offset);
    }
}
