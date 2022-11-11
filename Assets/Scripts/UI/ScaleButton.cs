using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScaleButton : MonoBehaviour, IPointerClickHandler
{
    public Settings settings;
    public Sprite largeScaleOn;
    public Sprite largeScaleOff;

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
        settings.largeScale = !settings.largeScale;
        updateVisuals();
    }

    private void updateVisuals () {
        if (settings.largeScale) {
            // Mute is on
            image.sprite = largeScaleOn;
        }
        else {
            // Mute is off
            image.sprite = largeScaleOff;
        }
    }
}
