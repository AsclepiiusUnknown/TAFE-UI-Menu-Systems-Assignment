using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public TextMeshProUGUI Up, Down, Left, Right, Shoot, Reload, Pause;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Shoot", KeyCode.Mouse0);
        keys.Add("Reload", KeyCode.R);
        keys.Add("Pause", KeyCode.Escape);

        for (int i = 0; i < keys.Count; i++)
        {
            keys.Add(i.ToString(), KeyCode.W);
            print(i);
        }
        Up.text = keys["Up"].ToString();
    }
}
