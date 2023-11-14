using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //showing question stuff
    public QuestionsAndAnswers QnA;
    public GameObject[] options;
    public TMP_Text QuestionTxt;

    private bool answered;
    
   
    private void Start()
    {
        answered = false;
        QnA.setUp();
        generateQuestion();
    }

    public void correct()
    {
        answered = true;
        generateQuestion(); 
    }

    

    void generateQuestion()
    {
        if (answered == false)
        {
            QuestionTxt.text = QnA.Question;
            setAnswers();
        }
        else{
            SceneManager.LoadScene("Open World Scene");
        }


    }




}
