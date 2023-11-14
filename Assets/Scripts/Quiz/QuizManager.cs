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

    //right or wrong answer
    public int correctButton;
    
   
    private void Start()
    {
        correctButton = QnA.setUp();
        generateQuestion();
    }

    public void correct()
    {
        SceneManager.LoadScene("Open World Scene");
    }

    public void wrong()
    {
        SceneManager.LoadScene("Open World Scene");
    }

    void generateQuestion()
    {   
        //writes questions
        QuestionTxt.text = QnA.Question;

        //will get from qA class and set buttons
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA.Answers[i];
        }
        options[correctButton].GetComponent<AnswerScript>().isCorrect = true;

    }




}
