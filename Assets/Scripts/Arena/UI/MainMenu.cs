using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject optionsUiHolder;

    public void Play()
    {
        SceneManager.LoadScene("Arena Game");
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
        mainMenuHolder.SetActive(false);
        optionsUiHolder.SetActive(true);
    }
}
