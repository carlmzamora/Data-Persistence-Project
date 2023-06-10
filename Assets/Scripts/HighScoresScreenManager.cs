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

    private int points;
    private int highScore;

    // Start is called before the first frame update
    void Start()
    {
        points = GameManager.Instance.currentPoints;
        highScore = GameManager.Instance.currentHighScore;

        if(points >= highScore) //show highscore dialog
        {
            gameOverDialog.SetActive(false);
            highScoreDialog.SetActive(true);
            highScoreText.text = points.ToString();
            GameManager.Instance.inputDisabled = true;
        }
        else //show gameover dialog
        {
            gameOverDialog.SetActive(true);
            highScoreDialog.SetActive(false);
            gameoverScoreText.text = points.ToString();
            GameManager.Instance.inputDisabled = false;
        }        
    }

    public void OnNameConfirm()
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
            GameManager.Instance.SaveHighScoreAndPlayer(points, highScorePlayerInput.text);
            highScorePressAnyKeyText.SetActive(true);
            validationText.SetActive(false);
            GameManager.Instance.inputDisabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
