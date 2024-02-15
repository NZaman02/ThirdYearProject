using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnZoo : MonoBehaviour
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
        StartCoroutine(LoadingZoo());
    }

    IEnumerator LoadingZoo()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Encyclopaedia");
        animator.SetTrigger("Start");
    }

}
