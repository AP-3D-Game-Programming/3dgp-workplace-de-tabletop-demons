using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 10f;
    public LayerMask interactLayer;
    private void Start()
    {
        // Verberg en vergrendel de cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
            {
                FollowerNPC npc = hit.collider.GetComponent<FollowerNPC>();
                if (npc != null)
                {
                    npc.ToggleFollow();
                    npc.player = transform; // speler doorgeven
                }
            }
        }
    }
}
