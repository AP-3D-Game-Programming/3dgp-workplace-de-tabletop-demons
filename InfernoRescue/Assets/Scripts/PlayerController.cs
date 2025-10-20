using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 55f;
    public float moveX;
    public float moveZ;
    private Rigidbody rb;

    public bool isGrounded = false;
    public float jumpForce = 10f;

    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransfrom;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransfrom = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Input
        moveX = Input.GetAxis("Vertical");
        moveZ = Input.GetAxis("Horizontal");

        RotateCamera();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = (transform.right * moveZ + transform.forward * moveX).normalized;
        Vector3 targetVelocity = movement * moveSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        if (isGrounded && moveZ == 0 && moveX == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void Jump()
    {
        isGrounded = false;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransfrom.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
