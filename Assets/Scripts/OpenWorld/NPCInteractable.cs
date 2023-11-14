using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NPCInteractable : MonoBehaviour
{
    public string animalName;

    public void Interact()
    {
        //activates quiz
        SceneManager.LoadScene("Quiz Scene");
      
        // Find the QuestionsAndAnswers in the scene
        QuestionsAndAnswers quizManager = FindObjectOfType<QuestionsAndAnswers>();
        if (quizManager != null)
        {
            // Call the method to pass the animalName to QuestionsAndAnswers
            quizManager.animalInteracted = animalName;
        }
    }


}
