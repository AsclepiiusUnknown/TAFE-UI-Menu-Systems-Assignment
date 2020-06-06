using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    #region Variables
    //Refernces to GameObjects to switch on/off depending on what we need visible
    [Header("UI GameObjects")]
    public GameObject mainMenuHolder;
    public GameObject pauseUiHolder;
    public GameObject optionsUiHolder;

    [Header("Settings Items")]
    //Used to check if the game is or isnt in fullscreen at any moment in time
    [HideInInspector]
    public bool isFullscreen = false;
    public Slider[] volumeSliders; //Array of sliders for volume control
    public Toggle[] resToggles; //An array of toggles that acts as a m=toggle group for selecting the resolution
    public int[] screenWidths; //An array of integers used to control the width of the screen when changing resolutions
    [HideInInspector]
    public int activeScreenResIndex; //the index value of the screen resolution currently being used
    [HideInInspector]
    public int sharedQualityIndex = 3; //index value of the screen quality

    [Header("Currently Unused")]
    public bool canCheer = true; //Used to check if the game is or isnt able to cheer at any moment in time (currently unused)
    #endregion

    //Called on the first frame the script is active
    private void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("Screen Res Index", 0); //Set the active screen resolution index to what it was saved to last time, if no data is found default it to 0

        bool isFullscreen = (PlayerPrefs.GetInt("Full Screen") == 1) ? true : false; //Make the game fullscreen if the fullscreen int is equal to 1, otherwise make it false (this uses a binary-based int system to evaluate the bool)

        //Set the values of each volume slider being used to the appropriate volume level from the Audio Manager
        volumeSliders[0].value = AudioManager.instance.masterVolPercent; //Master Volume
        volumeSliders[1].value = AudioManager.instance.musicVolPercent; //Music Volume
        volumeSliders[2].value = AudioManager.instance.sfxVolPercent; //SFX Volume

        for (int i = 0; i < resToggles.Length; i++) //For each of the resolution toggles
        {
            //Set the correct display of the toggles
            resToggles[i].isOn = i == activeScreenResIndex;
        }

        SetFullscreen(isFullscreen); //Set the view to fullscreen if needed
    }

    #region Other Menu Functionality
    //Used to go from the option back to the main menu
    public void GoToMainMenu()
    {
        mainMenuHolder.SetActive(true); //Enable the main menu UI
        optionsUiHolder.SetActive(false); //Disable the options UI
    }

    public void BackToPause()
    {
        Time.timeScale = 0;
        pauseUiHolder.SetActive(true);
        optionsUiHolder.SetActive(false);
    }
    #endregion

    #region Options Functionality
    //Used to set the screen reolution to a passed index
    public void SetScreenRes(int i)
    {
        if (resToggles[i].isOn) //If the UI element is on (using the passed index value)
        {
            activeScreenResIndex = i; //active screen res index is set to the passed index
            float aspectRatio = 16 / 9f; //Create the correct aspect ratio
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false); //Set the screen resolution to the correct values divided by the aspect ratio (not in fullscreen)
            PlayerPrefs.SetInt("Screen Res Index", activeScreenResIndex); //Save the active screen res index to the PlayerPrefs to access later
            PlayerPrefs.Save();
        }
    }

    //Used to turn fullscreen on/off using the passed boolean
    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resToggles.Length; i++) //For each of the resolution toggles
        {
            resToggles[i].interactable = !isFullscreen; //Set interactable to the opposite of the fullscreen value so that if fullscreen is on you cant interact with the toggles
        }

        if (isFullscreen) //If the view is fullscreen
        {
            Resolution[] allRes = Screen.resolutions; //Set variables for the res
            Resolution maxRes = allRes[allRes.Length - 1]; //Set the res to the max res
            Screen.SetResolution(maxRes.width, maxRes.height, true); //Set it using the function to automate the proccess
        }
        else //Otherwise if we arent in fullscreen
        {
            ///USED TO DEBUG:
            Debug.Log(activeScreenResIndex.ToString()); //Log the active screen res index
            if (0 <= activeScreenResIndex && activeScreenResIndex <= resToggles.Length) //If the active screen res index is greater than or equal to 0 AND its less than or equal to the numebr of res toggles
            {
                SetScreenRes(activeScreenResIndex); //Set the res to the active screen res index
            }
            else //Otherwise
            {
                SetScreenRes(0); //Set it to the default 0
            }
        }

        PlayerPrefs.SetInt("Full Screen", ((isFullscreen) ? 1 : 0)); //Save the fullscreen bool as an int using a binary-like system
        PlayerPrefs.Save(); //Save the PlayerPrefs
    }

    #region Volume Management
    //Set the master volume to the passed float value
    public void SetMasterVolume(float value)
    {
        //Set the volume using the Audio Manager
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    //Set the music volume to the passed float value
    public void SetMusicVolume(float value)
    {
        //Set the volume using the Audio Manager
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    //Set the SFX volume to the passed float value
    public void SetSfxVolume(float value)
    {
        //Set the volume using the Audio Manager
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.SFX);
    }
    #endregion

    //Used to set the cheers on/off using the passed bool (currently unused)
    public void SetCheers(bool _isCheering)
    {
        canCheer = !_isCheering; //switch can cheer
        PlayerPrefs.SetInt("Can Cheer", ((canCheer) ? 1 : 0)); //Set can cheer in player prefs
        PlayerPrefs.Save(); //Save Player Prefs
    }

    //Used to set the visual quality within the game using the passed quality index
    public void SetQuality(int qualityIndex)
    {
        sharedQualityIndex = qualityIndex; //shared quality index is the same as quality index but is a public variable to be accessed easily by other scripts
        QualitySettings.SetQualityLevel(qualityIndex); //Set the quality level accordingly
    }
    #endregion
}
