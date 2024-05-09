using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Variables
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject gameOverCanvas;

    public TMP_Text[] objectivesText;
    public TMP_Text ammoText;
    public TMP_Text healthText;
    public int ammo = 10;
    public int health = 100;
    public bool playerHasTheKeys = false;
    public bool playerHasTheFuel = false;

    bool isGamePaused = false;

    private void Awake()
    {
        Instance = this; 
    }

    private void Update()
    {
        ammoText.text = ammo.ToString();
        healthText.text = health.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void WinGame()
    {
        if(playerHasTheFuel == true && playerHasTheKeys == true)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("WinScene");
        }
    }

    void PauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
            pauseMenuCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
