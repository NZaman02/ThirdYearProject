using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnswerScript : MonoBehaviour
{

    public bool isCorrect;
    public QuizManager quizManager;

   public void Answer()
    {
        if (isCorrect)
        {
            quizManager.correct();

        }
        else
        {
            quizManager.wrong();
        }
    }



}
