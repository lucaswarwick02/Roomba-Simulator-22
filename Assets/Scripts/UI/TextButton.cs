using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Color normal = new Color(1f, 1f, 1f);
    private Color hovered = new Color(0.5377358f, 0.5377358f, 0.5377358f);
    private Color down = new Color(0.3604486f, 0.9433962f, 0.882103f);
    private Color disabled = new Color(0.3113208f, 0.3113208f, 0.3113208f, 0.5f);

    private TextMeshProUGUI textComp;
    private bool isHovered;

    private bool effectsEnabled = true;

    private void OnEnable() {
        OnPointerExit(null);
    }

    public void OnPointerEnter (PointerEventData eventData) {
        if (!effectsEnabled) return;
        isHovered = true;
        getTextComponent().color = hovered;
    }

    public void OnPointerExit (PointerEventData eventData) {
        if (!effectsEnabled) return;
        isHovered = false;
        getTextComponent().color = normal;
    }

    public void OnPointerDown (PointerEventData eventData) {
        if (!effectsEnabled) return;
        getTextComponent().color = down;
    }

    public void OnPointerUp (PointerEventData eventData) {
        if (!effectsEnabled) return;
        if (isHovered) {
            OnPointerEnter(null);
        }
        else {
            OnPointerExit(null);
        }
    }

    public void ToggleEffects (bool enabled) {
        effectsEnabled = enabled;
        if (effectsEnabled) {
            OnPointerExit(null);
        }
        else {
            getTextComponent().color = disabled;
        }
    }

    public void ForceDisable () {
        effectsEnabled = false;
    }

    private TextMeshProUGUI getTextComponent () {
        if (this.textComp == null) {
            this.textComp = GetComponent<TextMeshProUGUI>();
        }
        return this.textComp;
    }
}
