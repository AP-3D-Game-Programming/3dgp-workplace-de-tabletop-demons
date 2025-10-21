using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 10f;
    public LayerMask interactLayer;
    private Follower currentFolower;
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
                Follower npc = hit.collider.GetComponent<Follower>();
                if (npc != null)
                {
                    if(currentFolower!=null && currentFolower != npc)
                    {
                        currentFolower.isFollowing = false;
                        currentFolower.StopFollowing();
                    }
                    npc.ToggleFollow();
                    npc.player = transform;

                    if (npc.isFollowing)
                        currentFolower = npc;
                    else if (currentFolower == npc)
                        currentFolower = null;

                       
                }
            }
        }
    }
}
