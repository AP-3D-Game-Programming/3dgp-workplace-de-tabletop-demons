using UnityEngine;

public class ResidentCoughs : MonoBehaviour
{
    public float minInterval = 5f;
    public float maxInterval = 15f;

    private AudioSource src;
    private float nextCoughTime;
    
    void Start()
    {
       src = GetComponent<AudioSource>();
       src.playOnAwake = false;
       ScheduleNextCough();
    }

    
    void Update()
    {
        if (Time.time >= nextCoughTime)
        {
            src.PlayOneShot(src.clip);
            ScheduleNextCough();
        }
    }

    void ScheduleNextCough(){
        nextCoughTime = Time.time + Random.Range(minInterval, maxInterval);
    }
}
