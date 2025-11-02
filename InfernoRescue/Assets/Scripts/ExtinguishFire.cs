using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public float range = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            Extinguish();
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
                Debug.Log($"Fire detected: {hit.name}. Destroying...");
                Destroy(hit.gameObject);
                Debug.Log("Vuur geblust");
            }
        }
    }
}
