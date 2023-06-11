using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoresScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDialog;
    [SerializeField] private TextMeshProUGUI gameoverScoreText;
    [SerializeField] private GameObject highScoreDialog;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TMP_InputField highScorePlayerInput;
    [SerializeField] private GameObject highScorePressAnyKeyText;
    [SerializeField] private GameObject validationText;
    [SerializeField] private Transform table;
    [SerializeField] private EntryRow entryRowPrefab;

    private int points;
    private int highestHighScore;
    private int lowestHighScore;

    // Start is called before the first frame update
    void Start()
    {
        points = GameManager.Instance.currentPoints;
        highestHighScore = GameManager.Instance.highestHighScore;
        lowestHighScore = GameManager.Instance.lowestHighScore;
        highScorePlayerInput.ActivateInputField();

        if(points >= lowestHighScore) //eligible, show highscore dialog
        {
            gameOverDialog.SetActive(false);
            highScoreDialog.SetActive(true);
            highScoreText.text = points.ToString();
            GameManager.Instance.inputDisabled = true;
        }
        else if(points < lowestHighScore) //not eligible
        {
            gameOverDialog.SetActive(true);
            highScoreDialog.SetActive(false);
            gameoverScoreText.text = points.ToString();
            GameManager.Instance.inputDisabled = false;
        }

        PopulateTable();
    }

    public void OnNameConfirm() //only used in highscore dialog
    {
        string checkString = highScorePlayerInput.text;

        if (string.IsNullOrWhiteSpace(checkString)) //empty name
        {
            highScorePressAnyKeyText.SetActive(false);
            validationText.SetActive(true);
            GameManager.Instance.inputDisabled = true;
        }
        else //name has been inputted
        {
            GameManager.Instance.SaveHighScoreAndPlayer(highScorePlayerInput.text, points);

            highScorePressAnyKeyText.SetActive(true);
            validationText.SetActive(false);
            GameManager.Instance.inputDisabled = false;
            highScorePlayerInput.interactable = false;

            RepopulateTable();
        }
    }

    void PopulateTable()
    {
        for(int i = 0; i < GameManager.Instance.highScorePlayerList.Count; i++)
        {
            EntryRow row = Instantiate(entryRowPrefab, table).GetComponent<EntryRow>();
            row.UpdateInfo(i+1, GameManager.Instance.highScorePlayerList[i].playerName, GameManager.Instance.highScorePlayerList[i].score);
        }
    }

    void RepopulateTable()
    {
        for(int i = 0; i < GameManager.Instance.highScorePlayerList.Count; i++)
        {
            EntryRow row = table.GetChild(i).GetComponent<EntryRow>();
            row.UpdateInfo(i+1, GameManager.Instance.highScorePlayerList[i].playerName, GameManager.Instance.highScorePlayerList[i].score);
        }
    }
}
