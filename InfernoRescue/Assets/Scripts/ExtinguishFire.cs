using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public float range = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Extinguish();
        }
    }

    void Extinguish()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Fire"))
            {
                Destroy(hit.gameObject);
                Debug.Log("Vuur geblust");
            }
        }
    }
}
