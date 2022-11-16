using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CompletionPanel : MonoBehaviour
{
    public TextMeshProUGUI resultsTitle;

    public TextMeshProUGUI dirtCollected;
    public TextMeshProUGUI ringsCollected;
    public TextMeshProUGUI passScore;
    public TextMeshProUGUI medalScore;

    public TextMeshProUGUI score;

    [HideInInspector] public Level level;

    public Button nextLevelButton;
    public TextButton nextLevelText;

    public Image redFlag;
    public Image goldFlag;

    public Image tick1;
    public Image tick2;
    public Image medal;

    public void deactivateNextLevel () {
        nextLevelButton.enabled = false;
        nextLevelText.ToggleEffects(false);
        nextLevelText.ForceDisable();
    }

    public void NextLevel () {
        Debug.Log(level);
        Level nextLevel = level.NextLevel();
        SceneManager.LoadScene("Level_" + nextLevel.week + "-" + nextLevel.day);
    }

    public void RetryLevel () {
        SceneManager.LoadScene("Level_" + level.week + "-" + level.day);
    }

    public void MainMenu () {
        SceneManager.LoadScene("MainMenu");
    }
}
