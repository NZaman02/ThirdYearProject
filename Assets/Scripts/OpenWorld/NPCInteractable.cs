using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class NPCInteractable : MonoBehaviour
{
    public string animalName;


    public void Interact()
    {

        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);
        int questionsToPick = 1;
        foreach (string line in knowledgeData)
        {
            string[] values = line.Split(',');
            if (values[0] == animalName)
            {
                questionsToPick = int.Parse(values[1]);

                break;
            }
        }

        if(questionsToPick == 0)
        {
            PlayerPrefs.SetString("Animal", animalName);
            PlayerPrefs.SetInt("Answer", 2);
            SceneManager.LoadScene("Catch Animal");
        }
        else
        {
            //so quiz knows what animal
            PlayerPrefs.SetString("Animal", animalName);
            SceneManager.LoadScene("Quiz Scene");
        }





    }


}
