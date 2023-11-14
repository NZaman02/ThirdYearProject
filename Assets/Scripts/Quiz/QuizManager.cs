using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class QuizManager : MonoBehaviour
{
    //showing question stuff
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public TMP_Text QuestionTxt;


    //helps choose animals 
   
    private void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);

        generateQuestion();
    }


    void setAnswers()
    {
        //will get from qA class 
        for (int i = 0; i < options.Length; i++) {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent <AnswerScript>().isCorrect = true;
            }
        }
       
    }

    void generateQuestion()
    {
        //chooses random question
        if (QnA.Count > 0)
        {
            currentQuestion = UnityEngine.Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            setAnswers();
        }
        else
        {
            SceneManager.LoadScene("Open World Scene");
        }
       

    }




}
