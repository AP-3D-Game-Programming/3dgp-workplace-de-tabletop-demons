using UnityEngine;
using System.Collections; 
public class ExtinguishFire : MonoBehaviour
{
    public float range = 5f;
    public ParticleSystem waterSprayEffect;
    public GameObject smokeEffectPrefab;

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("F Pressed");
            if (waterSprayEffect != null && !waterSprayEffect.isPlaying)
            {
                waterSprayEffect.Play(); // Start het water-effect
            }
            Extinguish();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (waterSprayEffect != null)
            {
                waterSprayEffect.Stop(); // Stop het water-effect
            }
        }
    }

    void Extinguish()
    {
        Debug.Log($"Extinguish attempt at position {transform.position} with range {range}");
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        Debug.Log($"Number of colliders found: {hits.Length}");

        foreach (Collider hit in hits)
        {
            Debug.Log($"Hit object: {hit.name}, Tag: {hit.tag}");

            if (hit.CompareTag("Fire"))
            {
                hit.enabled = false;
                Debug.Log($"Fire detected: {hit.name}. Destroying...");
                if (smokeEffectPrefab != null)
                {
                    // Pak de positie van het vuur
                    Vector3 firePosition = hit.transform.position;

                    // Maak een kopie (instance) van het rook-prefab op de positie van het vuur
                    Instantiate(smokeEffectPrefab, firePosition, Quaternion.identity);
                }
                StartCoroutine(ShrinkAndDestroyFire(hit.gameObject));
                Debug.Log("Vuur geblust");
            }
        }
    }
    IEnumerator ShrinkAndDestroyFire(GameObject fireObject)
    {
        // Optioneel: Als je vuur-object een Particle System is (de vlammen),
        // kun je het vertellen te stoppen met het maken van *nieuwe* deeltjes.
        // De bestaande vlammen zullen dan natuurlijk uitdoven.
        ParticleSystem fireParticles = fireObject.GetComponent<ParticleSystem>();
        if (fireParticles != null)
        {
            fireParticles.Stop();
        }

        // --- Het krimp-proces ---
        float shrinkDuration = 2.0f; // Het vuur doet er 2 seconden over om te krimpen
        float timer = 0;

        Vector3 startScale = fireObject.transform.localScale;
        Vector3 endScale = Vector3.zero; 

        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / shrinkDuration;

            fireObject.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }


        fireObject.transform.localScale = endScale;
        Destroy(fireObject);
        Debug.Log("Vuur-object definitief vernietigd.");
    }
}
