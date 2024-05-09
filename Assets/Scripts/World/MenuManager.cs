using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject creditsCanvas;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject synopsisCanvas;
    [SerializeField] GameObject pauseMenuCanvas;

    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DisplayCredits()
    {
        creditsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void DisplayMainMenu()
    {
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void DisplaySynopsis()
    {
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        synopsisCanvas.SetActive(true);
    }

}
