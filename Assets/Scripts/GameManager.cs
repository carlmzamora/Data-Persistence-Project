using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentPoints;
    public int highestHighScore;
    public string currentHighScorePlayer;
    public int lowestHighScore;
    public bool inputDisabled = false;

    public List<Entry> highScorePlayerList = new List<Entry>();

    public static GameManager Instance;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadHighScoreAndPlayer();
    }

    void Update()
    {
        if(inputDisabled) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().name == SceneManager.GetSceneByBuildIndex(0).name)
            {
                SceneManager.LoadScene(1);
            }
            if(SceneManager.GetActiveScene().name == SceneManager.GetSceneByBuildIndex(2).name)
            {
                SceneManager.LoadScene(0);
                LoadHighScoreAndPlayer();
            }
        }
    }

    public void SaveHighScoreAndPlayer(string playerName, int score)
    {
        Entry entry = new Entry();
        entry.playerName = playerName;
        entry.score = score;

        highScorePlayerList.Add(entry);
        SortHighScorePlayerList();

        SaveData data = new SaveData();
        data.entries = highScorePlayerList;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/hscores.json", json);
    }

    void SortHighScorePlayerList()
    {
        if(highScorePlayerList.Count <= 1) return;

        highScorePlayerList.Sort((y,x) => x.score.CompareTo(y.score));

        if(highScorePlayerList.Count > 10)
        highScorePlayerList.RemoveAt(highScorePlayerList.Count-1);
    }

    public void LoadHighScoreAndPlayer()
    {
        string filePath = Application.persistentDataPath + "/hscores.json";
        if(!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        highScorePlayerList = data.entries;

        highestHighScore = highScorePlayerList[0].score;
        currentHighScorePlayer = highScorePlayerList[0].playerName;

        if(highScorePlayerList.Count == 10) //limited to 10 entries anyway
            lowestHighScore = highScorePlayerList[highScorePlayerList.Count-1].score;
        else if(highScorePlayerList.Count <= 9)
            lowestHighScore = 0;
    }
}

[System.Serializable]
class SaveData
{
    public List<Entry> entries;
}

[System.Serializable]
public class Entry
{
    public string playerName;
    public int score;
}
