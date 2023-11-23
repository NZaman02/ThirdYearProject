using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    //showing question stuff
    public QuestionsAndAnswers QnA;
    public GameObject[] options;
    public TMP_Text QuestionTxt;
    public Button[] buttonList;
    public TMP_Text animalNameText;

    //right or wrong answer
    public int correctButton;
    public Button continueButton;


    private void Start()
    {
        continueButton.onClick.AddListener(LoadSceneOnClick);
        continueButton.gameObject.SetActive(false);
        correctButton = QnA.setUp();

        generateQuestion();
    }

    public void correct()
    {
        PlayerPrefs.SetInt("Answer", 1);
        PlayerPrefs.Save();
        for (int i = 0; i < options.Length; i++)
        {
            if (i == correctButton)
            {
                ColorBlock colors = buttonList[i].colors;
                colors.normalColor = Color.green;
                colors.highlightedColor = Color.green;
                colors.selectedColor = Color.green;
                colors.disabledColor = Color.green;
                colors.pressedColor = Color.green;
                buttonList[i].colors = colors;
            }
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].interactable = false;


        }
        continueButton.gameObject.SetActive(true);
       
    }

    public void wrong()
    {
        PlayerPrefs.SetInt("Answer", 0);
        PlayerPrefs.Save();

        for (int i = 0; i < options.Length; i++)
        {
            if (i == correctButton)
            {
                ColorBlock colors = buttonList[i].colors;
                colors.normalColor = Color.green;
                colors.highlightedColor = Color.green;
                colors.selectedColor = Color.green;
                colors.disabledColor = Color.green;
                colors.pressedColor = Color.green;
                buttonList[i].colors = colors;
            }
            else
            {
                ColorBlock incorrectButtonColors = buttonList[i].colors;
                incorrectButtonColors.normalColor = Color.red;
                incorrectButtonColors.highlightedColor = Color.red;
                incorrectButtonColors.selectedColor = Color.red;
                incorrectButtonColors.disabledColor = Color.red;
                incorrectButtonColors.pressedColor = Color.red; 
                buttonList[i].colors = incorrectButtonColors;
            }
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].interactable = false;

        }
        continueButton.gameObject.SetActive(true);
       
    }

    private void LoadSceneOnClick()
    {
        SceneManager.LoadScene("Catch Animal");

    }

    void generateQuestion()
    {   
        //writes questions
        QuestionTxt.text = QnA.Question;

        //will get from qA class and set buttons
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            if (QnA.Answers[i] != "A")
            {
                options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA.Answers[i];
            }
            else
            {
                options[i].SetActive(false); // Set the button inactive because choice not available

            }
        }
        options[correctButton].GetComponent<AnswerScript>().isCorrect = true;
        animalNameText.text = PlayerPrefs.GetString("Animal");

    }




}
