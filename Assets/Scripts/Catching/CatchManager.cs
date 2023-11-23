using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class CatchManager : MonoBehaviour
{
    //set ui
    public TMP_Text dice1, dice2, bonus, amountNeeded, finalAmount, currentAnimalText;
    private int dice1Val, dice2Val, bonusVal, amountNeededVal;
    public Button continueButton;
    private string currentAnimal;

    //update knowledge
    public TextAsset playerKnowledgeText;

    private bool correctAns;

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

        if(finalAmountVal > amountNeededVal) 
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
        continueButton.gameObject.SetActive(true);

    }


    private void NotCaught()
    {
        continueButton.gameObject.SetActive(true);

    }

    private void LoadSceneOnClick()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
    }
}
