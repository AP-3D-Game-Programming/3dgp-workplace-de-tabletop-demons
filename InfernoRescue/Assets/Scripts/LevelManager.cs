using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Status")]
    private int totalResidents;
    private int residentsSaved = 0;
    public bool isIntroLevel = false; 

    [Header("Volgend Level")]
    public string nextLevelName = "Level1"; 

    [Header("UI Elementen")]
    public TextMeshProUGUI residentsSavedText; 
    public TextMeshProUGUI residentsRemainingText; 
    public GameObject transitionPanel; 
    public float transitionWaitTime = 3.0f; 

    public Image fadePanel;
    public float fadeDuration = 1.0f;

    void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (isIntroLevel)
        {
            if (residentsSavedText) residentsSavedText.gameObject.SetActive(false);
            if (residentsRemainingText) residentsRemainingText.gameObject.SetActive(false);
        }
        else
        {
            if (residentsSavedText) residentsSavedText.gameObject.SetActive(true);
            if (residentsRemainingText) residentsRemainingText.gameObject.SetActive(true);
        }

        if (transitionPanel) transitionPanel.SetActive(false);
        if (fadePanel) fadePanel.gameObject.SetActive(false);
    }

    public void SetTotalResidents(int total)
    {
        totalResidents = total;
        residentsSaved = 0;

        // Activeer de UI (niet in de intro)
        if (!isIntroLevel)
        {
            if (residentsSavedText) residentsSavedText.gameObject.SetActive(true);
            if (residentsRemainingText) residentsRemainingText.gameObject.SetActive(true);
            UpdateUI();
        }
    }

    public void RescueResident()
    {
        if (isIntroLevel)
        {
            StartCoroutine(IntroTransition());
            return;
        }

        residentsSaved++;
        UpdateUI();

        if (residentsSaved >= totalResidents)
        {
            Debug.Log("Alle bewoners gered! Level voltooid!");
            // StartCoroutine(LoadNextLevel(nextLevelName)); // Laad volgend level
        }
    }

    private void UpdateUI()
    {
        if (residentsSavedText)
        {
            residentsSavedText.text = $"Gered: {residentsSaved}";
        }
        if (residentsRemainingText)
        {
            int remaining = totalResidents - residentsSaved;
            residentsRemainingText.text = $"Resterend: {remaining}";
        }
    }

    // Coroutine voor de Intro-overgang
    private IEnumerator IntroTransition()
    {
        if (transitionPanel)
        {
            transitionPanel.SetActive(true);
        }

        yield return new WaitForSeconds(transitionWaitTime);

        if (transitionPanel)
        {
            transitionPanel.SetActive(false);
        }

        yield return StartCoroutine(FadeToScene(nextLevelName));
    }


    // De Fade-out Coroutine
    private IEnumerator FadeToScene(string sceneToLoad)
    {
        if (fadePanel == null)
        {
            Debug.LogWarning("FadePanel niet ingesteld, laad scene direct.");
            SceneManager.LoadScene(sceneToLoad);
            yield break; // Stop de coroutine hier
        }

        fadePanel.gameObject.SetActive(true);
        fadePanel.raycastTarget = true;

        float elapsedTime = 0f;
        Color color = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadePanel.color = new Color(color.r, color.g, color.b, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = new Color(color.r, color.g, color.b, 1f);

        // 4. Laad de scene
        SceneManager.LoadScene(sceneToLoad);
    }
}