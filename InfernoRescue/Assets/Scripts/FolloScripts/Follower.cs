using UnityEngine;
using UnityEngine.AI; 
public class Follower : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2f;
    public bool isFollowing = false;

    private NavMeshAgent agent;
    private Animator anim; 

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        UpdateAnimator();
    }
    void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;

       
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        
        float normalizedForwardSpeed = localVelocity.z / agent.speed;
        float normalizedStrafeSpeed = localVelocity.x / agent.speed;

       
        anim.SetFloat("Vert", normalizedForwardSpeed, 0.1f, Time.deltaTime);
        anim.SetFloat("Hor", normalizedStrafeSpeed, 0.1f, Time.deltaTime);

        
        if (agent.isOnOffMeshLink)
        {
            anim.SetBool("IsJump", true);
        }
        else
        {
            anim.SetBool("IsJump", false);
        }
    }
    public void ToggleFollow()
    {
        isFollowing = !isFollowing;
        if (!isFollowing)
        {
            agent.ResetPath();
            anim.SetFloat("Vert", 0);
            anim.SetFloat("Hor", 0);
        }
    }

    public void StopFollowing()
    {
        isFollowing = false;
        agent.ResetPath();
        anim.SetFloat("Vert", 0);
        anim.SetFloat("Hor", 0);
    }
}
