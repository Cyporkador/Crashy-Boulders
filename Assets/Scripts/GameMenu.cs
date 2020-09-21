using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private float volume;
    public bool muted;

    public bool gameOver;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        isPaused = false;
        muted = false;
        volume = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Resume();
            }

        }
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (muted)
            {
                muted = false;
                AudioListener.volume = volume;
            }
            else
            {
                muted = true;
                AudioListener.volume = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && gameOver)
        {
            Replay();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isPaused = false;
        gameOver = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Replay()
    {
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
