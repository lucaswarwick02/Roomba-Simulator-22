using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MuteButton : MonoBehaviour, IPointerClickHandler
{
    public Settings settings;
    public Sprite muteOn;
    public Sprite muteOff;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        updateVisuals();    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        settings.mute = !settings.mute;
        AudioListener.volume = settings.mute ? 0 : 1;
        updateVisuals();
    }

    private void updateVisuals () {
        if (settings.mute) {
            // Mute is on
            image.sprite = muteOn;
        }
        else {
            // Mute is off
            image.sprite = muteOff;
        }
    }
}
