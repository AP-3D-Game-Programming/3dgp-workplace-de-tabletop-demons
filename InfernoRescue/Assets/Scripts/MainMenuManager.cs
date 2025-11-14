using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;      
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public string introSceneName = "Tutorial";

    // Sleep hier je Opties-paneel naartoe in de Unity Editor
    public GameObject optionsPanel;

    public Image fadePanel;         
    public float fadeDuration = 1.0f;

    public AudioMixer mainMixer;    
    public Slider volumeSlider;
    void Start()
    {
        // Laad het opgeslagen volume, of gebruik '1' (max) als er nog niets is opgeslagen.
        float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 1f);

        // Stel de slider in op de juiste positie
        volumeSlider.value = savedVolume;

        // Stel het volume van de mixer in
        SetMasterVolume(savedVolume);
    }
    public void StartGame()
    {
        // Laadt de scene met de naam die je hebt opgegeven
        StartCoroutine(FadeToScene(introSceneName));
    }

    public void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true); // Maakt het optie-paneel zichtbaar
        }
    }

    // Gekoppeld aan een "Close" knop BINNEN je optie-paneel
    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false); // Maakt het optie-paneel onzichtbaar
        }
    }
    // Gekoppeld aan de "Quit" knop
    public void QuitGame()
    {
        // Dit werkt alleen in een gebouwde game (niet in de Unity Editor)
        Debug.Log("Spel wordt afgesloten..."); // Handig voor testen
        StartCoroutine(FadeToScene(null)); // null betekent 'afsluiten'
    }

    public void SetMasterVolume(float sliderValue)
    {
       
        float volumeInDB = Mathf.Log10(sliderValue) * 20;
        mainMixer.SetFloat("MasterVolume", volumeInDB);
        PlayerPrefs.SetFloat("SavedMasterVolume", sliderValue);
    }

    private IEnumerator FadeToScene(string sceneToLoad)
    {
        // 1. Maak het paneel actief en zorg dat het kliks vangt
        fadePanel.gameObject.SetActive(true);
        fadePanel.raycastTarget = true; // Blokkeer nu wel de knoppen

        // 2. De Fade-out loop
        float elapsedTime = 0f;
        Color color = fadePanel.color; // Huidige kleur (met alpha 0)

        while (elapsedTime < fadeDuration)
        {
            // Bereken de nieuwe alpha
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            // Pas de kleur aan (r, g, b, nieuwe alpha)
            fadePanel.color = new Color(color.r, color.g, color.b, newAlpha);

            // Tel de tijd op
            elapsedTime += Time.deltaTime;

            // Wacht 1 frame
            yield return null;
        }

        // 3. Zorg dat het 100% zwart is aan het einde
        fadePanel.color = new Color(color.r, color.g, color.b, 1f);

        // 4. Laad de scene OF sluit het spel af
        if (sceneToLoad != null)
        {
            // Laad de nieuwe scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Sluit het spel af
            Debug.Log("Spel wordt afgesloten...");
            Application.Quit();
        }
    }
}
