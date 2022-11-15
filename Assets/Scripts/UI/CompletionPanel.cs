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
    public TextMeshProUGUI dirtScore;
    public TextMeshProUGUI ringsScore;

    public TextMeshProUGUI score;

    [HideInInspector] public Level level;

    public Button nextLevelButton;
    public TextButton nextLevelText;

    public Image redFlag;
    public Image goldFlag;

    public void deactivateNextLevel () {
        nextLevelButton.enabled = false;
        nextLevelText.ToggleEffects(false);
        nextLevelText.ForceDisable();
    }

    public void NextLevel () {
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
