using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System;

public class CatchManager : MonoBehaviour
{
    //set ui
    public TMP_Text dice1, dice2, bonus, amountNeeded, finalAmount, currentAnimalText, result, continueText;
    private int dice1Val, dice2Val, bonusVal, amountNeededVal;
    public Button continueButton;
    private string currentAnimal;

    //update knowledge
    private bool correctAns;

    // Start is called before the first frame update
    void Start()
    {

        //set up catching context
        continueButton.gameObject.SetActive(false);
        currentAnimal = PlayerPrefs.GetString("Animal");
        correctAns = PlayerPrefs.GetInt("Answer") == 1;


        //set up dice mechanic 
        dice1Val = UnityEngine.Random.Range(1, 7);
        dice2Val = UnityEngine.Random.Range(1, 7);
        if(correctAns)
        {
            bonusVal = 3;
        }
        else
        {
            bonusVal = 0;
        }
        amountNeededVal = 8;

        int finalAmountVal = dice1Val + dice2Val + bonusVal;

        //do text
        dice1.text = dice1Val.ToString();
        dice2.text = dice2Val.ToString();
        bonus.text = bonusVal.ToString();
        amountNeeded.text = amountNeededVal.ToString();
        finalAmount.text = finalAmountVal.ToString();
        currentAnimalText.text = currentAnimal;

        if (finalAmountVal >= amountNeededVal) 
        {
            Caught();
        }
        else
        {
            NotCaught();
        }

    }

    private void Caught()
    {
        //feedback catch
        continueButton.gameObject.SetActive(true);
        result.text = "CAUGHT";
        //work out current player knowledge
        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);


        List<string> animalNames = new List<string>();
        List<string> playerLevels = new List<string>();

        //read and score
        foreach (string line in knowledgeData)
        {
            string[] values = line.Split(',');

            animalNames.Add(values[0]);
            playerLevels.Add(values[1]);
        }

        // update
        for (int i = 0; i < animalNames.Count; i ++)
        {
            if (animalNames[i] == currentAnimal)
            {
                //sets max level
                if (int.Parse(playerLevels[i]) < 5)
                {
                    playerLevels[i] = (int.Parse(playerLevels[i]) + 1).ToString();
                }
            }
        }

        //can now write back to file
        TextWriter tw = new StreamWriter(filePath, false);
        for (int i = 0; i < animalNames.Count; i ++)
        {
            tw.WriteLine(animalNames[i] + "," + playerLevels[i]);
        }
        tw.Close();
        continueButton.onClick.AddListener(LoadCardSceneOnClick);
        continueText.text = "See New Information Unlock";
    }


    private void NotCaught()
    {
        continueButton.onClick.AddListener(LoadOpenSceneOnClick);
        continueButton.gameObject.SetActive(true);
        result.text = "FAILED";
        continueText.text = "Return To Open World";
    }

    private void LoadOpenSceneOnClick()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
    }

    private void LoadCardSceneOnClick()
    {
        SceneManager.LoadScene("AnimalCard");
    }
}

