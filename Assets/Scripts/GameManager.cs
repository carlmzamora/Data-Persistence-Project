using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentPoints;
    public int currentHighScore;
    public string currentHighScorePlayer;
    public bool inputDisabled = false;

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

    [System.Serializable]
    class SaveData
    {
        public int hscore;
        public string player;
    }

    public void SaveHighScoreAndPlayer(int hscore, string player)
    {
        SaveData data = new SaveData();
        data.hscore = hscore;
        data.player = player;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/hscores.json", json);
    }

    public void LoadHighScoreAndPlayer()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/hscores.json");

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        currentHighScore = data.hscore;
        currentHighScorePlayer = data.player;
    }
}
