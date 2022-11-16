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

    public static TextMeshProUGUI needtext1;
    public TextMeshProUGUI needtext2;
    public Image needMedal1;
    public Image needMedal2;
    public static int medalAmount;
    public TextMeshProUGUI medalAmountT;

    private void Start() {
        UpdateUI();
        medalAmountT.text = "x " + medalAmount.ToString();
        
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
    /// (De)activates the UI based on the current GameSave data.
    /// </summary>
    public void UpdateUI () {

        // medalAmountT.text = "x " + medalAmount.ToString();
        
        AudioListener.volume = settings.mute ? 0 : 1;

        week1Option.GetComponent<Button>().enabled = gameProgress.IsWeekUnlocked(1);
        week1Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsWeekUnlocked(1));

        week2Option.GetComponent<Button>().enabled = gameProgress.IsWeekUnlocked(2);
        week2Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsWeekUnlocked(2));
        // needtext1.enabled = !(gameProgress.IsWeekUnlocked(2));

        week3Option.GetComponent<Button>().enabled = gameProgress.IsWeekUnlocked(3);
        week3Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsWeekUnlocked(3));

        for (int i = 0; i < gameProgress.week1Levels.Length; i++) {
            week1Levels[i].GetComponent<Button>().enabled = gameProgress.IsDayUnlocked(1, i + 1);
            week1Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(1, i + 1));
            week1Levels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ("high score " + gameProgress.week1Levels[i].percentage);
            week1Levels[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(1, i + 1));
            week1Levels[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
            // week1Levels[1].transform.GetChild(2).GetComponent< = gameProgress.IsDayUnlocked(1, i + 1);
        }

        for (int i = 0; i < gameProgress.week2Levels.Length; i++) {
            week2Levels[i].GetComponent<Button>().enabled = gameProgress.IsDayUnlocked(2, i + 1);
            week2Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(2, i + 1));
            week2Levels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ("high score " + gameProgress.week2Levels[i].percentage);
            week2Levels[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(2, i + 1));
            week2Levels[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
        }

        for (int i = 0; i < gameProgress.week3Levels.Length; i++) {
            week3Levels[i].GetComponent<Button>().enabled = gameProgress.IsDayUnlocked(3, i + 1);
            week3Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(3, i + 1));
            week3Levels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ("high score " + gameProgress.week3Levels[i].percentage);
            week3Levels[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsDayUnlocked(3, i + 1));
            week3Levels[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
        }
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
