using TMPro;
using UnityEngine;

public class OxygenTimer : MonoBehaviour
{
    public TextMeshProUGUI oxygenText;

    public float maxOxygen = 100f;

    public float depletionRate = 1f;

    public float fireDamageRate = 5f;
    public float refillRate = 10f;

    private float currentOxygen;
    private bool isInFire = false;
    private bool isOutOfBreath = false;
    private bool isOutside = false;


    void Start()
    {
        currentOxygen = maxOxygen;
        UpdateOxygenDisplay();
    }

    public void ForceExitFire()
    {
        if (isInFire)
        {
            isInFire = false;
        }
    }

    void Update()
    {
        if (currentOxygen > 0)
        {
            if (isOutside && !isInFire)
            {
                currentOxygen += refillRate * Time.deltaTime;
                currentOxygen = Mathf.Min(maxOxygen, currentOxygen);
            } else
            {
                currentOxygen -= depletionRate * Time.deltaTime;
            }

            if (isInFire)
            {
                currentOxygen -= fireDamageRate * Time.deltaTime;
            }

            currentOxygen = Mathf.Max(0, currentOxygen);

            UpdateOxygenDisplay();
        } else
        {
            if (!isOutOfBreath)
            {
                isOutOfBreath = true;
                LevelManager.Instance.TriggerGameOver("You ran out of oxygen!");
                Debug.Log("Zuurstof is op");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("In vuur");
            isInFire = true;
        }

        if (other.CompareTag("OutsideTrigger"))
        {
            Debug.Log("Buiten");
            isOutside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("Uit vuur");
            isInFire = false;
        }
        if (other.CompareTag("OutsideTrigger"))
        {
            Debug.Log("Binnen");
            isOutside = false;
        }
    }

    void UpdateOxygenDisplay()
    {
        int oxygenPercentage = Mathf.RoundToInt(currentOxygen);

        oxygenText.text = string.Format("Oxygen: {0}", oxygenPercentage);
    }
}
