using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CompletionPanel : MonoBehaviour
{
    [HideInInspector] public Level level;

    public GameObject nextLevel;

    [Header("Image Components")]
    public Image tick1;
    public Image tick2;
    public Image medal;

    [Header("Text Components")]
    public TextMeshProUGUI resultsTitle;
    public TextMeshProUGUI dirtCollected;
    public TextMeshProUGUI ringsCollected;
    public TextMeshProUGUI passScore;
    public TextMeshProUGUI medalScore;
    public TextMeshProUGUI hc;
    public TextMeshProUGUI points;

    [Header("Sound Effects")]
    [SerializeField] public AudioSource win;
    [SerializeField] public AudioSource lose;
    [SerializeField] public AudioSource click;

    public void removeNextLevelButton () {
        Destroy(nextLevel);
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
