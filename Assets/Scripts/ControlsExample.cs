using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsExample : MonoBehaviour
{
    public Sprite[] controls;

    private bool isDown;

    float timer = 0f;
    float maxTimer = 0.75f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= maxTimer) {
            isDown = !isDown;
            GetComponent<Image>().sprite = controls[isDown ? 1 : 0];
            timer = 0f;
        }
    }
}
