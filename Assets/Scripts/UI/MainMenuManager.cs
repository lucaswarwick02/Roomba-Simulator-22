using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameProgress gameProgress;
    public Settings settings;

    public GameObject mainMenuPanel;

    public GameObject week1Panel;
    public GameObject week2Panel;
    public GameObject week3Panel;

    public GameObject week1Option;
    public GameObject week2Option;
    public GameObject week3Option;

    public GameObject[] week1Levels;
    public GameObject[] week2Levels;
    public GameObject[] week3Levels;

    private void Start() {
        UpdateUI();
    }

    public void SelectWeek1 () {
        DeactivateAllPanels();
        week1Panel.SetActive(true);
    }
    public void SelectWeek2 () {
        DeactivateAllPanels();
        week2Panel.SetActive(true);
    }
    public void SelectWeek3 () {
        DeactivateAllPanels();
        week3Panel.SetActive(true);
    }

    /// <summary>
    /// Go back to the MainMenu panel.
    /// </summary>
    public void Back () {
        DeactivateAllPanels();
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Create a new save.
    /// </summary>
    public void ResetProgress () {
        // ! THIS DOES NOTHING CURRENTLY
        UpdateUI();
    }

    /// <summary>
    /// (De)activates the UI based on the current GameSave data.
    /// </summary>
    public void UpdateUI () {

        week1Option.GetComponent<Button>().enabled = gameProgress.IsWeekUnlocked(1);
        week1Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsWeekUnlocked(1));

        week2Option.GetComponent<Button>().enabled = gameProgress.IsWeekUnlocked(2);
        week2Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsWeekUnlocked(2));

        week3Option.GetComponent<Button>().enabled = gameProgress.IsWeekUnlocked(3);
        week3Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsWeekUnlocked(3));

        for (int i = 0; i < gameProgress.week1Levels.Length; i++) {
            week1Levels[i].GetComponent<Button>().enabled = gameProgress.IsDayUnlocked(1, i + 1);
            week1Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(1, i + 1));
            week1Levels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (gameProgress.week1Levels[i].percentage * 100) + "%";
            week1Levels[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(1, i + 1));
            week1Levels[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
        }

        for (int i = 0; i < gameProgress.week2Levels.Length; i++) {
            week2Levels[i].GetComponent<Button>().enabled = gameProgress.IsDayUnlocked(2, i + 1);
            week2Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(2, i + 1));
            week2Levels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (gameProgress.week2Levels[i].percentage * 100) + "%";
            week2Levels[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(2, i + 1));
            week2Levels[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
        }

        for (int i = 0; i < gameProgress.week3Levels.Length; i++) {
            week3Levels[i].GetComponent<Button>().enabled = gameProgress.IsDayUnlocked(3, i + 1);
            week3Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(3, i + 1));
            week3Levels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (gameProgress.week3Levels[i].percentage * 100) + "%";
            week3Levels[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(3, i + 1));
            week3Levels[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
        }
        Debug.Log(gameProgress.week1Levels[1].percentage + " - " + gameProgress.week1Levels[1].percentage);
    }

    public void StartLevel (string levelID) {
        // Load level scene
        SceneManager.LoadScene("Level_" + levelID);
    }

    private void DeactivateAllPanels () {
        week1Panel.SetActive(false);
        week2Panel.SetActive(false);
        week3Panel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }
}
