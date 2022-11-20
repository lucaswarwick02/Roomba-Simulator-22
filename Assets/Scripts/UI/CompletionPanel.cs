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
    public Image coverUp;
    public TextMeshProUGUI hc;

    [SerializeField] public AudioSource win;
    [SerializeField] public AudioSource lose;

    [SerializeField] public AudioSource click;

    [HideInInspector] public Level level;

    public Button nextLevelButton;
    public TextButton nextLevelText;

    public Image redFlag;
    public Image goldFlag;

    public Image tick1;
    public Image tick2;
    public Image medal;
    public TextMeshProUGUI getPointsForNL;
    public Image emptyTick1;

    /// <summary>
    /// Deactivate the next level button.
    /// </summary>
    public void deactivateNextLevel()
    {
        nextLevelButton.enabled = false;
        nextLevelText.ToggleEffects(false);
        nextLevelText.ForceDisable();
    }

    /// <summary>
    /// Change the scene to the next level.
    /// </summary>
    public void NextLevel()
    {
        click.Play();
        Level nextLevel = level.NextLevel();
        SceneManager.LoadScene("Level_" + nextLevel.house + "-" + nextLevel.room);
    }

    /// <summary>
    /// Restart the level.
    /// </summary>
    public void RetryLevel()
    {
        click.Play();
        SceneManager.LoadScene("Level_" + level.house + "-" + level.room);
    }

    /// <summary>
    /// Return to the main menu.
    /// </summary>
    public void MainMenu()
    {
        click.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
