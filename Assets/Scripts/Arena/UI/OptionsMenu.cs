using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject pauseUiHolder;
    public GameObject optionsUiHolder;

    [HideInInspector]
    public bool isFullscreen;

    public Slider[] volumeSliders;
    public Toggle[] resToggles;
    public int[] screenWidths;
    [HideInInspector]
    public int activeScreenResIndex;
    [HideInInspector]
    public int sharedQualityIndex = 3;

    public bool canCheer = true;


    private void Start()
    {
        //activeScreenResIndex = PlayerPrefs.GetInt("Screen Res Index");
        //isFullscreen = (PlayerPrefs.GetInt("Full Screen") == 1)?true:false;

        volumeSliders[0].value = AudioManager.instance.masterVolPercent;
        volumeSliders[1].value = AudioManager.instance.musicVolPercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolPercent;

        ///
        for (int i = 0; i < resToggles.Length; i++)
        {
            resToggles[i].isOn = i == activeScreenResIndex;
        }
        ///

        SetFullscreen(isFullscreen);
    }
    public void GoToMainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionsUiHolder.SetActive(false);
    }
    public void SetScreenRes(int i)
    {
        if (resToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            //PlayerPrefs.SetInt("Screen Res Index", activeScreenResIndex);
            //PlayerPrefs.Save();
        }
    }
    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resToggles.Length; i++)
        {
            resToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allRes = Screen.resolutions;
            Resolution maxRes = allRes[allRes.Length - 1];
            Screen.SetResolution(maxRes.width, maxRes.height, true);
        }
        else
        {
            SetScreenRes(activeScreenResIndex);
        }

        //PlayerPrefs.SetInt("Full Screen", ((isFullscreen) ? 1 : 0));
        //PlayerPrefs.Save();
    }
    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }
    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }
    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.SFX);
    }
    public void SetCheers(bool _isCheering)
    {
        if (_isCheering)
        {
            _isCheering = false;
            canCheer = false;
            //PlayerPrefs.SetInt("Can Cheer", ((canCheer) ? 1 : 0));
        }
        else
        {
            _isCheering = true;
            canCheer = true;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        sharedQualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void BackToPause()
    {
        Time.timeScale = 0;
        pauseUiHolder.SetActive(true);
        optionsUiHolder.SetActive(false);
    }
}
