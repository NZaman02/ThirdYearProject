using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

public class QuizManager : MonoBehaviour
{
    //showing question stuff
    public QuestionsAndAnswers QnA;
    public GameObject[] options;
    public TMP_Text QuestionTxt;
    public Button[] buttonList;
    public TMP_Text animalNameText;
    public Image animalImage;

    //csv
    private float startTime, endTime; 


    //right or wrong answer
    public int correctButton;
    public Button continueButton;


    private void Start()
    {
        //set up images
        string data = PlayerPrefs.GetString("Animal");
        string imagePath = $"Assets/Sprites/Animals/{data}.png";

        Texture2D texture = LoadTexture(imagePath);

        if (texture != null)
        {
            Sprite animalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            animalImage.sprite = animalSprite;
        }

        continueButton.onClick.AddListener(LoadSceneOnClick);
        continueButton.gameObject.SetActive(false);
        correctButton = QnA.setUp();

        startTime = Time.time;

        generateQuestion(); 
    }

    public void correct()
    {
        endTime = Time.time;
        PlayerPrefs.SetInt("Answer", 1);
        PlayerPrefs.Save();
        for (int i = 0; i < options.Length; i++)
        {
            if (i == correctButton)
            {
                ColorBlock colors = buttonList[i].colors;
                colors.normalColor = Color.green;
                colors.highlightedColor = Color.green;
                colors.selectedColor = Color.green;
                colors.disabledColor = Color.green;
                colors.pressedColor = Color.green;
                buttonList[i].colors = colors;
            }
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].interactable = false;


        }
        continueButton.gameObject.SetActive(true);
       
    }

    public void wrong()
    {
        endTime = Time.time;
        PlayerPrefs.SetInt("Answer", 0);
        PlayerPrefs.Save();

        for (int i = 0; i < options.Length; i++)
        {
            if (i == correctButton)
            {
                ColorBlock colors = buttonList[i].colors;
                colors.normalColor = Color.green;
                colors.highlightedColor = Color.green;
                colors.selectedColor = Color.green;
                colors.disabledColor = Color.green;
                colors.pressedColor = Color.green;
                buttonList[i].colors = colors;
            }
            else
            {
                ColorBlock incorrectButtonColors = buttonList[i].colors;
                incorrectButtonColors.normalColor = Color.red;
                incorrectButtonColors.highlightedColor = Color.red;
                incorrectButtonColors.selectedColor = Color.red;
                incorrectButtonColors.disabledColor = Color.red;
                incorrectButtonColors.pressedColor = Color.red; 
                buttonList[i].colors = incorrectButtonColors;
            }
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].interactable = false;

        }
        continueButton.gameObject.SetActive(true);
       
    }

    private void LoadSceneOnClick()
    {
        //update player qs done
        string filePath = Path.Combine(Application.persistentDataPath, "playerStats.csv");

        //work out values
       

        string GotCorrect = PlayerPrefs.GetInt("Answer", 0).ToString(); 

        string timeTaken = (endTime - startTime).ToString();

        string animalChosen = PlayerPrefs.GetString("Animal");

        string whichQ = "";
        switch (QuestionTxt.text)
        {
            case "What is this animal's conservation status":
                whichQ = animalChosen + ".1";
                break;
            case "What is this animal's latin name?":
                whichQ = animalChosen + ".2";
                break;
            case "What type of diet does this animal follow?":
                whichQ = animalChosen + ".3";
                break;
            case "What is the average lifespan for this animal in the WILD?":
                whichQ = animalChosen + ".4";
                break;
            case "What is the average lifespan for this animal in CAPTIVITY?":
                whichQ = animalChosen + ".5";
                break;
            case "What is the average WEIGHT for this animal?":
                whichQ = animalChosen + ".6";
                break;
            case "What is the average LENGTH for this animal?":
                whichQ = animalChosen + ".7";
                break;
            case "What is the average HEIGHT for this animal?":
                whichQ = animalChosen + ".8";
                break;
            case "How many offspring does the animal have on average?":
                whichQ = animalChosen + ".9";
                break;
            case "Who is this animal's predators?":
                whichQ = animalChosen + ".10";
                break;
        }

        // Check if the file exists; 
        if (!File.Exists(filePath))
        {   
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("WhichQ,GotCorrect,TimeTaken"); // Header
            }
        }

        // Append data to the CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))
            {
            string csvLine = string.Format("{0},{1},{2}", whichQ, GotCorrect, timeTaken);
            writer.WriteLine(csvLine);
        }


        SceneManager.LoadScene("Catch Animal");

    }

    void generateQuestion()
    {   
        //writes questions
        QuestionTxt.text = QnA.Question;

        //will get from qA class and set buttons
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            if (QnA.Answers[i] != "A")
            {
                options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA.Answers[i];
            }
            else
            {
                options[i].SetActive(false); // Set the button inactive because choice not available

            }
        }
        options[correctButton].GetComponent<AnswerScript>().isCorrect = true;
        animalNameText.text = PlayerPrefs.GetString("Animal");

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
