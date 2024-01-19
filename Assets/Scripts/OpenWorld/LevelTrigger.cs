using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelTrigger : MonoBehaviour
{

    public string SceneDestination;
    public string CurrentScene;
    public GameObject sceneManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetString("CameFrom", CurrentScene);
            SceneManager.LoadScene(SceneDestination, LoadSceneMode.Single);
           
        }
       
    }
}
