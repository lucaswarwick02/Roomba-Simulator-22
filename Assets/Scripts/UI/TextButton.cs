using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color normal;
    public Color hovered;
    public Color down;

    private TextMeshProUGUI textComp;
    private bool isHovered;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<TextMeshProUGUI>();
        OnPointerExit(null);
    }

    public void OnPointerEnter (PointerEventData eventData) {
        isHovered = true;
        textComp.color = hovered;
    }

    public void OnPointerExit (PointerEventData eventData) {
        isHovered = false;
        textComp.color = normal;
    }

    public void OnPointerDown (PointerEventData eventData) {
        textComp.color = down;
    }

    public void OnPointerUp (PointerEventData eventData) {
        if (isHovered) {
            OnPointerEnter(null);
        }
        else {
            OnPointerExit(null);
        }
    }
}
