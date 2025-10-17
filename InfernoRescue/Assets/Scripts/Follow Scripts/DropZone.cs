using UnityEngine;

public class DropZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FollowerNPC npc = other.GetComponent<FollowerNPC>();
        if (npc != null)
        {
            npc.StopFollowing();
            Debug.Log("NPC afgeleverd in dropzone!");
        }
    }
}
