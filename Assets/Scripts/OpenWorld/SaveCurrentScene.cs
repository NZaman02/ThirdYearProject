using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCurrentScene : MonoBehaviour
{
    public string currentScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Save PlayerPrefs data whenever a new scene is loaded
        SavePlayerPrefsData();
    }

    private void SavePlayerPrefsData()
    {
        PlayerPrefs.SetString("PrevScene", currentScene);
        PlayerPrefs.Save();

    }
}
