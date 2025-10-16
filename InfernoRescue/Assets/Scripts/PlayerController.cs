using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 55f;
    public float moveX;
    public float moveZ;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        //Input
        moveX = Input.GetAxis("Vertical");
        moveZ = Input.GetAxis("Horizontal");


    }

    private void FixedUpdate()
    {
        //Movement
        Vector3 forwardMove = transform.forward * moveX * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMove);

        //Rotation
        float turn = moveZ * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
