using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MuteButton : MonoBehaviour
{
    public Settings settings;

    TextMeshProUGUI buttonText;

    private void Awake() {
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        updateVisuals();    
    }

    public void ToggleMute () {
        settings.mute = !settings.mute;
        AudioListener.volume = settings.mute ? 0 : 1;
        updateVisuals();
    }

    private void updateVisuals () {
        buttonText.text = "Mute: " + (settings.mute ? "On" : "Off");
    }
}
