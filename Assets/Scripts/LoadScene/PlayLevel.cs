using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void Start()
    {
        SaveManager.instance.fileLoaded += OnLoad;
    }

    public  void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "play" + (setup.LastLevel + 1);
    }

    private void OnDestroy()
    {
        SaveManager.instance.fileLoaded -= OnLoad;

    }
}
