using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables
    //Refernces to GameObjects to switch on/off depending on what we need visible
    [Header("UI GameObjects")]
    public GameObject mainMenuHolder;
    public GameObject optionsUiHolder;
    #endregion

    #region Menu Functionality
    //Used to start a game
    public void Play()
    {
        SceneManager.LoadScene("Arena Game");
    }

    //used to go to the options menu
    public void GoToOptionsMenu()
    {
        mainMenuHolder.SetActive(false); //disable the main menu UI
        optionsUiHolder.SetActive(true); //enable the options UI
    }

    //Used to quit game
    public void Quit()
    {
#if UNITY_EDITOR //If it is being run in unity editor
        UnityEditor.EditorApplication.isPlaying = false; //Stop editor simulation
#endif

        Application.Quit(); //Stop the application if there is one
    }
    #endregion
}
