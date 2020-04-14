using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameUiHolder;
    public GameObject pauseUiHolder;
    public GameObject optionsUiHolder;

    public bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
    public void GoToOptionsMenu()
    {
        pauseUiHolder.SetActive(false);
        optionsUiHolder.SetActive(true);
        gameUiHolder.SetActive(false);
    }

    public void GoToMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            gameUiHolder.SetActive(true);
            pauseUiHolder.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            gameUiHolder.SetActive(false);
            pauseUiHolder.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        gameUiHolder.SetActive(true);
        pauseUiHolder.SetActive(false);
    }

    public void BackToPause()
    {
        Time.timeScale = 0;
        gameUiHolder.SetActive(false);
        pauseUiHolder.SetActive(true);
        optionsUiHolder.SetActive(false);
    }
}
