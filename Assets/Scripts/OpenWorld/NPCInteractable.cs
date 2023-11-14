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
        SceneManager.LoadScene("Quiz Scene");
        // Find the QuizManager in the scene
        QuizManager quizManager = FindObjectOfType<QuizManager>();
        if (quizManager != null)
        {
            // Call the method to pass the animalName to QuizManager
            quizManager.animalInteracted = animalName;
        }

    }


}
