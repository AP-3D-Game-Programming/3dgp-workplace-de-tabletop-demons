using UnityEngine;

public class Dropzone : MonoBehaviour
{
    public GameObject vfxEffect; // verwijzing naar het Particle System object
    public AudioSource audioSource; // AudioSource voor het geluid
    private ParticleSystem particleSystem;

    private void Start()
    {
        if (vfxEffect != null)
        {
            particleSystem = vfxEffect.GetComponent<ParticleSystem>();
            if (particleSystem == null)
            {
                Debug.LogWarning("Geen ParticleSystem component gevonden op het opgegeven object.");
            }
            else
            {
                particleSystem.Stop(); // zorg dat het effect niet automatisch afspeelt
            }
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        Follower npc = other.GetComponent<Follower>();
        if (npc != null)
        {
            npc.StopFollowing();
            Debug.Log("NPC afgeleverd in dropzone!");

            // Speel het Particle System af
            if (particleSystem != null)
            {
                particleSystem.Play();
            }

            // Speel het geluid af
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
