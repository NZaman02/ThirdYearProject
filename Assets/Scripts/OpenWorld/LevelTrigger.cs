using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{

    public string SceneDestination;
    public string CurrentScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetString("PrevScene", CurrentScene);
            Debug.Log(PlayerPrefs.GetString("PrevScene"));
            SceneManager.LoadScene(SceneDestination, LoadSceneMode.Single);
        }
    }
}
