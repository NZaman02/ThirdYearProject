using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Threading;
using UnityEngine.Windows;

public class CatchManager : MonoBehaviour
{
    //set ui
    public TMP_Text bonus, amountNeeded, finalAmount, currentAnimalText, result, continueText, question, correctAnswer;
    public TextAsset answerBankText;
    private int dice1Val, dice2Val, bonusVal, amountNeededVal;
    public Button continueButton;
    public Image animalImage;
    private string currentAnimal;
    public Animator animatorDice1,animatorDice2;

    //update knowledge
    private int correctAns;

    // Start is called before the first frame update
    void Start()
    {
        string data = PlayerPrefs.GetString("Animal");
        string imagePath = $"Assets/Sprites/Animals/{data}.png";

        Texture2D texture = LoadTexture(imagePath);

        if (texture != null)
        {
            Sprite animalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            animalImage.sprite = animalSprite;
        }

        continueButton.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("Answer") == 2)
        {
            question.color = new Color(question.color.r, question.color.g, question.color.b, 0);
            correctAnswer.color = new Color(question.color.r, question.color.g, question.color.b, 0);
        }
        else{
            //dislay q and correct ans
            string filePath = Path.Combine(Application.persistentDataPath, "playerStats.csv");
            string[] allLines = System.IO.File.ReadAllLines(filePath);
            string lastLine = allLines[allLines.Length - 1];
            string[] values = lastLine.Split(',');
            string qtype = values[0].Trim();
            string[] parts = qtype.Split('.');
            string questionWas = parts[1];

            switch (questionWas)
            {
                case "1":
                    question.text = "Conservation status: ";
                    break;
                case "2":
                    question.text = "Latin name: ";
                    break;
                case "3":
                    question.text = "Diet: ";
                    break;
                case "4":
                    question.text = "Average lifespan in the WILD: ";
                    break;
                case "5":
                    question.text = "Average lifespan in CAPTIVITY: ";
                    break;
                case "6":
                    question.text = "Average WEIGHT: ";
                    break;
                case "7":
                    question.text = "Average LENGTH: ";
                    break;
                case "8":
                    question.text = "Average HEIGHT: ";
                    break;
                case "9":
                    question.text = "Average Offspring: ";
                    break;
                case "10":
                    question.text = "Predators: ";
                    break;

            }
            string[] answerBank = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
            correctAnswer.text = answerBank[(int.Parse(parts[0]) * 12) + int.Parse(parts[1])];
        }


        StartCoroutine(SetupCatchingContext());

    }

    IEnumerator SetupCatchingContext()
    {
        continueButton.gameObject.SetActive(false);


        //set up catching context
        currentAnimal = PlayerPrefs.GetString("Animal");
        correctAns = PlayerPrefs.GetInt("Answer");


        //set up dice mechanic 
        dice1Val = UnityEngine.Random.Range(1, 7);
        dice2Val = UnityEngine.Random.Range(1, 7);
        switch (correctAns)
        {
            case 0:
                bonusVal = 0;
                bonus.color = Color.red;
                correctAnswer.color = Color.red;
                break;
            case 1:
                bonusVal = 3;
                bonus.color = Color.green;
                correctAnswer.color = Color.green;  
                break;
            case 2:
                bonusVal = 5;
                bonus.text = "First Catch Bonus";
                break;
            default:
                bonusVal = 0;
                bonus.color = Color.red;
                break;
        }
        amountNeededVal = 8;

        int finalAmountVal = dice1Val + dice2Val + bonusVal;

        if (correctAns == 2)
        {
            currentAnimalText.text = "First Catch For: " + currentAnimal;
        }
        else
        {
            currentAnimalText.text = currentAnimal;
        }

        //do text
        bonus.text = bonusVal.ToString();
        amountNeeded.text = amountNeededVal.ToString();
       
        animatorDice1.SetBool("Rolling", true);
        animatorDice2.SetBool("Rolling", true);
        yield return new WaitForSeconds(3);

        //for animator
        animatorDice1.SetInteger("D1 Roll", dice1Val) ;
        animatorDice1.SetBool("Rolling", false);
        animatorDice2.SetInteger("D1 Roll", dice2Val);
        animatorDice2.SetBool("Rolling", false);
        finalAmount.text = finalAmountVal.ToString();

        if (finalAmountVal >= amountNeededVal) 
        {
            result.color = Color.green;
            Caught();
        }
        else
        {
            result.color = Color.red;
            NotCaught();
        }
        continueButton.gameObject.SetActive(true);

    }

    private void Caught()
    {
        //feedback catch
        continueButton.gameObject.SetActive(true);
        result.text = "CAUGHT";
        //work out current player knowledge
        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = System.IO.File.ReadAllLines(filePath);


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
        bool finAnimal = false; 
        for (int i = 0; i < animalNames.Count; i ++)
        {
            if (animalNames[i] == currentAnimal)
            {
                if ((int.Parse(playerLevels[i])) == 5) 
                {
                    finAnimal = true;
                }

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

        if (finAnimal)
        {
            continueButton.onClick.AddListener(LoadOpenSceneOnClick);
            continueText.text = "Animal Already Completed";
        }
        else
        {
            continueButton.onClick.AddListener(LoadCardSceneOnClick);
            continueText.text = "See New Information Unlock";
        }
       
    }


    private void NotCaught()
    {
        continueButton.onClick.AddListener(LoadOpenSceneOnClick);
        continueButton.gameObject.SetActive(true);
        result.text = "UNLUCKY TRY AGAIN";
        continueText.text = "Return To Open World";
    }

    private void LoadOpenSceneOnClick()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
    }

    private void LoadCardSceneOnClick()
    {
        PlayerPrefs.SetString("JustCaught", "True");
        SceneManager.LoadScene("AnimalCard");
    }

    private Texture2D LoadTexture(string path)
    {
        try
        {
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
        catch
        {
            byte[] fileData = System.IO.File.ReadAllBytes($"Assets/Sprites/Animals/NA.png");
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
    }


}

