using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().name == SceneManager.GetSceneByBuildIndex(0).name)
            {
                SceneManager.LoadScene(1);
            }
            if(SceneManager.GetActiveScene().name == SceneManager.GetSceneByBuildIndex(2).name)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
