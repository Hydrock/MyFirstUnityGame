using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;      // Игрок (капсула)
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Смещение камеры
    public float followSpeed = 10f;  // Скорость следования

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        transform.LookAt(target);
    }
}
