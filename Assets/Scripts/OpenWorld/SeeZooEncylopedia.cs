using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{
    public string currentScene;
    private void Start()
    {
        // Attach the button click event listener
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    // Handle button click event
    void OnButtonClick()
    {
        PlayerPrefs.SetString("PrevScene", currentScene);
        SceneManager.LoadScene("Encylopedia Scene");
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
