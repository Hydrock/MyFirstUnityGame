using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion camRotation = Quaternion.Euler(0f, targetAngle, 0f);
            moveInput = camRotation * Vector3.forward;

            // Поворот капсулы
            transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, 0.15f);
        }
        else
        {
            moveInput = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        // Сохраняем текущую вертикальную скорость
        Vector3 velocity = moveInput.normalized * speed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }
}
