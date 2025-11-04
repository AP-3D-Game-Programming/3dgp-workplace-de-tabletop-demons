using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Target Settings")]
    public Transform player;
    public Transform playerHead;

    [Header("Camera Settings")]
    public float smoothSpeed = 10f;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerHead == null || player == null)
        {
            transform.rotation = playerHead.rotation;

            Vector3 desiredPos = playerHead.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        }
    }
}
