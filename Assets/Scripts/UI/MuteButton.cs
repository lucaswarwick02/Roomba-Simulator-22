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

    /// <summary>
    /// Toggle sound based on game settings.
    /// </summary>
    public void ToggleMute () {
        settings.mute = !settings.mute;
        AudioListener.volume = settings.mute ? 0 : 1;
        updateVisuals();
    }

    /// <summary>
    /// Update the visuals to match the game settings.
    /// </summary>
    private void updateVisuals () {
        buttonText.text = "Mute: " + (settings.mute ? "On" : "Off");
    }
}
