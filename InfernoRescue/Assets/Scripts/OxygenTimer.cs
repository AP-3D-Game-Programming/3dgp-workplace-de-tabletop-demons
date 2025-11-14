using TMPro;
using UnityEngine;

public class OxygenTimer : MonoBehaviour
{
    public TextMeshProUGUI oxygenText;

    public float maxOxygen = 100f;

    public float depletionRate = 1f;

    public float fireDamageRate = 5f;

    private float currentOxygen;
    private bool isInFire = false;


    void Start()
    {
        currentOxygen = maxOxygen;
        UpdateOxygenDisplay();
    }

    void Update()
    {
        if (currentOxygen > 0)
        {
            currentOxygen -= depletionRate * Time.deltaTime;

            if (isInFire)
            {
                currentOxygen -= fireDamageRate * Time.deltaTime;
            }

            currentOxygen = Mathf.Max(0, currentOxygen);

            UpdateOxygenDisplay();
        } else
        {
            Debug.Log("Zuurstof is op");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("In vuur");
            isInFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("Uit vuur");
            isInFire = false;
        }
    }

    void UpdateOxygenDisplay()
    {
        int oxygenPercentage = Mathf.RoundToInt(currentOxygen);

        oxygenText.text = string.Format("Oxygen: {0}", oxygenPercentage);
    }
}
