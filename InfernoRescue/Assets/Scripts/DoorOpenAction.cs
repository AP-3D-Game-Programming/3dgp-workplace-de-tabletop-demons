using UnityEngine;

public class DoorOpenAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float interactDistance = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance))
            {
                DoorInteraction door = hit.transform.GetComponentInParent<DoorInteraction>();
                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }
    }
}
