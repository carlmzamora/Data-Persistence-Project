using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_HighScore;
    private string m_HighScorePlayer;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        m_HighScore = GameManager.Instance.highestHighScore;
        m_HighScorePlayer = GameManager.Instance.currentHighScorePlayer;
        Debug.Log(m_HighScore + " " + m_HighScorePlayer);
        UpdateHighScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reload
                SceneManager.LoadScene(2);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        GameManager.Instance.currentPoints = m_Points;

        UpdateHighScore();
    }

    void UpdateHighScore()
    {
        if(m_Points > m_HighScore)
        {
            m_HighScore = m_Points;
            m_HighScorePlayer = "NEW!";
        }
        GameManager.Instance.highestHighScore = m_HighScore;

        if (m_HighScore <= 0) //no high score yet
        {
            HighScoreText.gameObject.SetActive(false);
        }
        else
        {
            HighScoreText.gameObject.SetActive(true);
            HighScoreText.text = $"High Score: {m_HighScore}  |  {m_HighScorePlayer}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
