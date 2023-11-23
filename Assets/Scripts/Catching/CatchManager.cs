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
    public TMP_Text dice1, dice2, bonus, amountNeeded, finalAmount, currentAnimalText, result;
    private int dice1Val, dice2Val, bonusVal, amountNeededVal;
    public Button continueButton;
    private string currentAnimal;

    //update knowledge
    private bool correctAns;
    public TextAsset playerKnowledge;

    // Start is called before the first frame update
    void Start()
    {

        //set up catching context
        continueButton.onClick.AddListener(LoadSceneOnClick);
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

        if (finalAmountVal > amountNeededVal) 
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
        //string[] knowledgeData = File.ReadAllLines(filePath);
       
        
        string[] knowledgeData = playerKnowledge.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = knowledgeData.Length;
        List<string> animalNames = new List<string>();
        List<string> playerLevels = new List<string>();

        //read and score
        for (int i = 0; i < numOfAnimal-1; i +=2)
        {
            animalNames.Add(knowledgeData[i].ToString());
            playerLevels.Add(knowledgeData[i + 1].ToString());
        }

        // update
        for (int i = 0; i < (numOfAnimal - 1)/2; i ++)
        {
            if (animalNames[i] == currentAnimal)
            {
                playerLevels[i] = (int.Parse(playerLevels[i]) + 1).ToString();
            }
        }

        //can now write back to file
        TextWriter tw = new StreamWriter(filePath, false);
        for (int i = 0; i < (numOfAnimal - 1)/2; i ++)
        {
            tw.WriteLine(animalNames[i] + "," + playerLevels[i] + "\n");
        }
        tw.Close();
        Debug.Log("File written to: " + filePath);
    }


    private void NotCaught()
    {
        continueButton.gameObject.SetActive(true);
        result.text = "FAILED";

    }

    private void LoadSceneOnClick()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
    }
}
