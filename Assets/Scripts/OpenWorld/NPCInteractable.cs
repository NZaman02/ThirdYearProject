using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class NPCInteractable : MonoBehaviour
{
    public string animalName;

    public void Interact()
    {
        //so quiz knows what animal
        PlayerPrefs.SetString("Animal", animalName);
        //activates quiz
        SceneManager.LoadScene("Quiz Scene");

     

    }


}
