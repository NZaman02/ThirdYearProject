using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{

    public string SceneDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DontDestroyOnLoad(collision.gameObject);
            SceneManager.LoadScene(SceneDestination, LoadSceneMode.Single);
        }
    }
}
