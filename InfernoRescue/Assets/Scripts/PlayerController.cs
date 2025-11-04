using UnityEngine;
public class Player : MonoBehaviour
{
    // Ground Movement
    public float moveSpeed = 5f;
    private float moveX;
    private float moveZ;
    private Rigidbody rb;

    // Jump Movement
    public bool isGrounded = true;
    public float jumpForce = 6f;
    public float fallMultiplier = 2.5f;
    public float ascendMultiplier  = 2f;
    private float playerHeight;
    public LayerMask groundLayer;

    // Camera Rotation
    public float mouseSensitivity = 1f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // Set the raycast underneath the player feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;

        //Locks the cursor
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
        ApplyJumpPhysics();


        Vector3 spherePosition = transform.position + Vector3.down * (playerHeight/2 - 0.1f);
        float sphereRadius = 0.3f;
        isGrounded = Physics.CheckSphere(spherePosition, sphereRadius, groundLayer ); 
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
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }

    void Jump()
    {
        isGrounded = false;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
