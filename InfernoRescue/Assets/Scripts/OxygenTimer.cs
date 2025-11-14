using TMPro;
using UnityEngine;

public class OxygenTimer : MonoBehaviour
{
    public TextMeshProUGUI oxygenText;

    public float maxOxygen = 100f;

    public float depletionRate = 1f;

    private float currentOxygen;


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

            currentOxygen = Mathf.Max(0, currentOxygen);

            UpdateOxygenDisplay();
        } else
        {
            Debug.Log("Zuurstof is op");
        }
    }

    void UpdateOxygenDisplay()
    {
        int oxygenPercentage = Mathf.RoundToInt(currentOxygen);

        oxygenText.text = string.Format("Oxygen: {0}", oxygenPercentage);
    }
}
