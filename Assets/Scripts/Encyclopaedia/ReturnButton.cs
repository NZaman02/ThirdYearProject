using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{

    public Animator animator;
    private void Start()
    {
        // Attach the button click event listener
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    // Handle button click event
    void OnButtonClick()
    {
        StartCoroutine(LoadingOpen());
       
    }

    IEnumerator LoadingOpen()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
        animator.SetTrigger("Start");
    }

}
