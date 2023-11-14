using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //showing question stuff
    public QuestionsAndAnswers QnA;
    public GameObject[] options;
    public TMP_Text QuestionTxt;


    //helps choose animals 
   
    private void Start()
    {
        QnA.setUp();
        generateQuestion();
    }

    void setAnswers()
    {
        //will get from qA class 
        for (int i = 0; i < options.Length; i++) {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA.Answers[i];
        }
       
    }

    void generateQuestion()
    {
        QuestionTxt.text = QnA.Question;
        setAnswers();
        SceneManager.LoadScene("Open World Scene");
    }




}
