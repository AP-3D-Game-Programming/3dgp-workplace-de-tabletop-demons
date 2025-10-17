using UnityEngine;

public class Dropzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Follower npc = other.GetComponent<Follower>();
        if (npc != null)
        {
            npc.StopFollowing();
            Debug.Log("NPC afgeleverd in dropzone!");
        }
    }
}
