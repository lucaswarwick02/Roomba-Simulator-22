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

    private bool isEnabled;

    private void Awake() {
        textComp = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ToggleEffects(true);
    }

    public void OnPointerEnter (PointerEventData eventData) {
        if (!isEnabled) return;
        isHovered = true;
        textComp.color = hovered;
    }

    public void OnPointerExit (PointerEventData eventData) {
        if (!isEnabled) return;
        isHovered = false;
        textComp.color = normal;
    }

    public void OnPointerDown (PointerEventData eventData) {
        if (!isEnabled) return;
        textComp.color = down;
    }

    public void OnPointerUp (PointerEventData eventData) {
        if (!isEnabled) return;
        if (isHovered) {
            OnPointerEnter(null);
        }
        else {
            OnPointerExit(null);
        }
    }

    public void ToggleEffects (bool enabled) {
        if (enabled) {
            OnPointerExit(null);
            isEnabled = true;
        }
        else {
            isEnabled = false;
            textComp = GetComponent<TextMeshProUGUI>();
            textComp.color = disabled;
        }
    }
}
