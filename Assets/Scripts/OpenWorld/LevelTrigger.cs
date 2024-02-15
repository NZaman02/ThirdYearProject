using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelTrigger : MonoBehaviour
{

    public string SceneDestination;
    public string CurrentScene;
    public GameObject sceneManager;
    public Animator animator; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetString("CameFrom", CurrentScene);
            StartCoroutine(LoadingOpen());
        }
       
    }


    IEnumerator LoadingOpen()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneDestination, LoadSceneMode.Single);
        animator.SetTrigger("Start");
    }

}
