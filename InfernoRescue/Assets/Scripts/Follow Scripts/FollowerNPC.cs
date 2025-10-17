using UnityEngine;

public class FollowerNPC : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2f;
    public bool isFollowing = false;

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (isFollowing && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    public void ToggleFollow()
    {
        isFollowing = !isFollowing;
        if (!isFollowing)
        {
            agent.ResetPath();
        }
    }

    public void StopFollowing()
    {
        isFollowing = false;
        agent.ResetPath();
    }
}
