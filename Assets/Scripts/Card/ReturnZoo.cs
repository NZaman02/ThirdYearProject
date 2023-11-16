using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnZoo : MonoBehaviour
{
    private void Start()
    {
        // Attach the button click event listener
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    // Handle button click event
    void OnButtonClick()
    {
        SceneManager.LoadScene("Encyclopaedia");
    }
}
