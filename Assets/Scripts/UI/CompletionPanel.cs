using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CompletionPanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI percentageText;

    public void BackToMainMenu () {
        SceneManager.LoadScene("MainMenu");
    }
}
