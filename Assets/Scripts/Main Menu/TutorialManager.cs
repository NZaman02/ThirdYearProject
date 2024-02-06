using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour

{
    public Button nextButton;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(continueButton);

    }

    // Update is called once per frame
    void continueButton()
    {
        SceneManager.LoadScene("Grasslands");
    }
}
