using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

///NOTE: PAB stands for Press Any Button and the abbreviation is used without this script///

public class PAB : MonoBehaviour
{
    #region Variables
    //Refernces to GameObjects to switch on/off depending on what we need visible
    [Header("UI GameObjects")]
    public GameObject menuUI, PABUI;

    [Header("PAB Components")]
    private bool isPAB; //If we are currently in the PAB Screen
    public TextMeshProUGUI PabTitle; //TMPro GUI text element to display PAB flash text
    private Color lerpedFlash; //Color that our flash text will flash to from its original color
    [TextArea] //Creates a propper text field in the inspector
    public string flashText; //Stores the string we want to display and flash within our PabTitle
    #endregion

    #region Start & Text Blinking
    //Called on the first frame our script is active
    private void Start()
    {
        isPAB = true; //We always want PAB visible and active upon start
        PabTitle.text = flashText; //Set the text element to display our string

        StartCoroutine(BlinkText());//Start the coroutine that flashes our text element
    }

    //Used to make the PAB text blink
    public IEnumerator BlinkText()
    {
        //Blink it as long as PAB is visible and active (You can set a terminating condition depending upon your requirement)
        while (isPAB)
        {
            //set the Text's text to blank
            PabTitle.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.5f);
            //display flash text for the next 0.5 seconds
            PabTitle.text = flashText;
            yield return new WaitForSeconds(.5f);
        }
    }
    #endregion

    #region Input Detection for Changing to Menu
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && isPAB == true) //If the user has pressed ANY key AND PAB is on (so we dont do it more than once)
        {
            Debug.Log("A key or mouse click has been detected"); //Print that a key was pressed in the console
            if (PABUI != null && menuUI != null) //If the PAB AND Menu UI objects aren't null
            {
                PABUI.SetActive(false); //disable the PAB UI
                menuUI.SetActive(true); //enable the menu UI
                isPAB = false; //We are no longer able to press any button
                Debug.Log("PAB -> Menu: Successful"); //Log the transition as successful
            }
            else //Otherwise
            {
                //DEBUGGING:
                if (PABUI == null) //if the PABUI is null
                {
                    Debug.LogError("PABUI for PAB.cs not found"); //Log the error
                }
                else if (menuUI == null) //if the menu UI is null
                {
                    Debug.LogError("menuUI for PAB.cs not found"); //Log the error
                }
            }
        }
    }
    #endregion
}
