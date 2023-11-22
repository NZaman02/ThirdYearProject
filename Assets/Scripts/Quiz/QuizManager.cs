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
    
   
    private void Start()
    {
        correctButton = QnA.setUp();
        generateQuestion();
    }

    public void correct()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
    }

    public void wrong()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
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
