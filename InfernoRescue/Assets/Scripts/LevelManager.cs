using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [Header("Game Timer")]
    public float levelDurationInSeconds = 300f; // 5 minuten
    public TextMeshProUGUI timerText; 
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("Game Over & Doden")]
    public int maxDeaths = 3;
    private int deathsCount = 0;
    private bool isGameOver = false;

    [Header("UI Feedback")]
    public GameObject deathFeedbackPanel; 
    public float deathFeedbackDuration = 1.5f;
    public GameObject gameOverPanel; // Je "Game Over" scherm
    public TextMeshProUGUI gameOverReasonText;

    [Header("Scene Namen")]
    public string currentLevelName; 
    public string mainMenuName = "MainMenu"; 

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
      
        isGameOver = false;
        Time.timeScale = 1f; 
        currentTime = levelDurationInSeconds; // Zet de timer
        deathsCount = 0; // Reset de doden

        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (deathFeedbackPanel) deathFeedbackPanel.SetActive(false);
       
        if (isIntroLevel)
        {
            if (residentsSavedText) residentsSavedText.gameObject.SetActive(false);
            if (residentsRemainingText) residentsRemainingText.gameObject.SetActive(false);
            if (timerText) timerText.gameObject.SetActive(false); // Verberg timer in intro
        }
        else
        {
            isTimerRunning = true; // Start de timer
            if (residentsSavedText) residentsSavedText.gameObject.SetActive(true);
            if (residentsRemainingText) residentsRemainingText.gameObject.SetActive(true);
            if (timerText) timerText.gameObject.SetActive(true);
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
    void Update()
    {
        // Als de timer niet loopt of het spel voorbij is, doe niets
        if (!isTimerRunning || isGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            TriggerGameOver("Tijd is om!");
        }

        UpdateTimerUI(currentTime);
    }

    private void UpdateTimerUI(float time)
    {
        if (timerText == null) return;

        time = Mathf.Max(time, 0); // Zorg dat de timer nooit negatief wordt

        // Formatteer de tijd naar Minuten:Seconden
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void RegisterDeath()
    {
        if (isGameOver) return; // Als het spel al voorbij is, tel niet verder

        deathsCount++;
        StartCoroutine(ShowDeathFeedback()); // Toon de "Bewoner verloren!"-flits

        // Check of de limiet is bereikt
        if (deathsCount >= maxDeaths)
        {
            TriggerGameOver("Te veel bewoners verloren!");
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
    private IEnumerator ShowDeathFeedback()
    {
        if (deathFeedbackPanel == null) yield break; // Stop als er geen paneel is

        deathFeedbackPanel.SetActive(true);
        yield return new WaitForSeconds(deathFeedbackDuration);
        deathFeedbackPanel.SetActive(false);
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
    public void TriggerGameOver(string reason)
    {
        if (isGameOver) return; // Zorg dat dit maar één keer gebeurt

        isGameOver = true;
        isTimerRunning = false;
        Time.timeScale = 0f; // Pauzeert ALLES (belangrijk!)

        // Toon Game Over scherm
        if (gameOverPanel)
        {
            gameOverReasonText.text = reason; // Zet de juiste reden neer
            gameOverPanel.SetActive(true);
        }

        // Maak de cursor weer zichtbaar
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Zet de tijd weer op normaal VOORDAT je laadt!
        SceneManager.LoadScene(currentLevelName);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Zet de tijd weer op normaal
        SceneManager.LoadScene(mainMenuName);
    }
}