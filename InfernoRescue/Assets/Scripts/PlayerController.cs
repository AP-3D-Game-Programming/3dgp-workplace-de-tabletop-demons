using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -10f;
    public float moveForward;
    public float moveHorizontal;
    private Rigidbody rb;

    public float jumpForce = 1.5f;
    public Vector3 velocity;
    public bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        moveForward = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 move = transform.right* moveHorizontal + transform.forward*moveForward;

        velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
