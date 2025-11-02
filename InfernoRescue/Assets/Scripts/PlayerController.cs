using UnityEngine;
public class Player : MonoBehaviour
{
    // Ground Movement
    public float moveSpeed = 5f;
    public float turnSpeed = 55f;
    public float moveX;
    public float moveZ;
    private Rigidbody rb;

    // Jump Movement
    public bool isGrounded = true;
    public float jumpForce = 6f;
    public float fallMulitplier = 2.5f;
    public float ascendMultiplier  = 2f;
    private float playerHeight;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float raycastDistance;
    public LayerMask groundLayer;

    // Camera Rotation
    public float mouseSensitivity = 1f;
    private float verticalRotation = 0f;
    private Transform cameraTransfrom;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransfrom = Camera.main.transform;

        // Set the raycast underneath the player feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight/ 2) + 0.3f;

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


        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
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
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMulitplier * Time.fixedDeltaTime;
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

        cameraTransfrom.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
