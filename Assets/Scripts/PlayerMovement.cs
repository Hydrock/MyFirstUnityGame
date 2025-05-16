using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 6f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Управление движением
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion camRotation = Quaternion.Euler(0f, targetAngle, 0f);
            moveInput = camRotation * Vector3.forward;

            // Поворот игрока
            transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, 0.1f);
        }
        else
        {
            moveInput = Vector3.zero;
        }

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = moveInput.normalized * speed;
        velocity.y = rb.linearVelocity.y; // сохраняем вертикальное ускорение
        rb.linearVelocity = velocity;
    }

    void OnCollisionStay(Collision collision)
    {
        // Простейшая проверка "на земле"
        if (collision.contacts.Length > 0 && Vector3.Angle(collision.contacts[0].normal, Vector3.up) < 45f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
