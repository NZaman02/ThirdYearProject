using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LootboxQuizManager : MonoBehaviour
{

    public TextAsset answerBankText;

    public Button animal1,animal2, animal3, answer1, answer2, answer3, reset, returnOpenWorld;
    public TMP_Text animal1Text, animal2Text, animal3Text, answer1Text, answer2Text, answer3Text,result;


    //what has been pressed
    private int[] pair1,pair2,pair3;
    private int numclicked;

    //questions chosen and where they go 
    private int[,] answersToUse,ansPairs;
    private float startTime, endTime;


    // Start is called before the first frame update
    void Start()
    {
        pair1 = new int[] { 0, 0 };
        pair2 = new int[] { 0, 0 };
        pair3 = new int[] { 0, 0 };

        numclicked = 0;
        returnOpenWorld.gameObject.SetActive(false);
        Color textColor = result.color;
        textColor.a = 0;
        result.color = textColor;

        startTime = Time.time;
        answersToUse = setupQuestions();
        ansPairs = setUpButtons(answersToUse);

        animal1.onClick.AddListener(() => addClickedButton(animal1));
        animal2.onClick.AddListener(() => addClickedButton(animal2));
        animal3.onClick.AddListener(() => addClickedButton(animal3));
        answer1.onClick.AddListener(() => addClickedButton(answer1));
        answer2.onClick.AddListener(() => addClickedButton(answer2));
        answer3.onClick.AddListener(() => addClickedButton(answer3));
        reset.onClick.AddListener(resetButton);
        returnOpenWorld.onClick.AddListener(LoadOpenSceneOnClick);

    }

    private void LoadOpenSceneOnClick()
    {
        string sceneName = PlayerPrefs.GetString("PrevScene");
        SceneManager.LoadScene(sceneName);
    }

    private void resetButton()
    {
        numclicked = 0;
        animal1.image.color = Color.white;
        animal2.image.color = Color.white;
        animal3.image.color = Color.white;
        answer1.image.color = Color.white;
        answer2.image.color = Color.white;
        answer3.image.color = Color.white;
    }

    private void updateStats(int GotCorrect)
    {
        string timeTaken = (endTime - startTime).ToString();

        string filePath = Path.Combine(Application.persistentDataPath, "playerStats.csv");
        // Check if the file exists; 
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("WhichQ,GotCorrect,TimeTaken, QType"); // Header
            }
        }

        for (int i = 0; i < 3; i++)
        {
            string whichQ = answersToUse[i,0].ToString() +"." +answersToUse[i,1].ToString();


            // Append data to the CSV file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                string csvLine = string.Format("{0},{1},{2},{3}", whichQ, GotCorrect, timeTaken,"2");
                writer.WriteLine(csvLine);
            }

        }


    }


    private void checkAns()
    {
        endTime = Time.time;
        reset.gameObject.SetActive(false);

        int[][] clickedButtons = new int[][] { pair1, pair2, pair3 };
      
        bool allFound = true;
        for (int x = 0; x < 3; x++)
        {
            int[] correctPair = new int[] { ansPairs[x, 0], ansPairs[x, 1] };
            bool isCorrect = false;  

            for (int y = 0; y < 3; y++)
            {
                if (correctPair.SequenceEqual(clickedButtons[y]))
                {
                    isCorrect = true;
                    break; 
                }
            }

            if (!isCorrect)
            {
                allFound = false;
            }
            
            
        }
        Color textColor = result.color;
        textColor.a = 255;
        result.color = textColor;
        if (allFound)
        {
            result.text = "Correct";
            PlayerPrefs.SetInt("JetpackUses", 3);
            updateStats(1);
        }
        else
        {
            result.text = "Wrong";

            //shows correct answer
            for (int x = 0;x< 3; x++)
            {
                Color currentCol = Color.white;
                switch (x) { 
                    case 0:
                        currentCol = Color.red; break;
                    case 1: 
                        currentCol = Color.green; break;    
                    case 2:
                        currentCol= Color.blue; break;  
                
                }

                switch(ansPairs[x, 0])
                {
                    case 1:
                        animal1.image.color = currentCol; break;
                    case 2:
                        animal2.image.color = currentCol ; break;
                    case 3:
                        animal3.image.color = currentCol; break;

                }

                switch (ansPairs[x, 1])
                {
                    case 1:
                        answer1.image.color = currentCol; break;
                    case 2:
                        answer2.image.color = currentCol; break;
                    case 3:
                        answer3.image.color = currentCol; break;

                }



            }
            updateStats(0);

        }

    }

    void Update()
    {
        if(numclicked == 6  )
        {
            checkAns();
            numclicked = 7;
        }    
        if (numclicked == 7)
        {
            returnOpenWorld.gameObject.SetActive(true);

        }
    }


    public void addClickedButton(Button button)
    {
        if (numclicked < 1) {
            pair1[0] = (whichButton(button.name));
            button.image.color = Color.blue;
            numclicked++;
        }
        else if (numclicked < 2)
        {
            pair1[1] = (whichButton(button.name));
            button.image.color = Color.blue;
            numclicked++;

        }
        else if (numclicked < 3)
        {
            pair2[0] = (whichButton(button.name));
            button.image.color = Color.green;
            numclicked++;
        }
        else if (numclicked < 4)
        {
            pair2[1] = (whichButton(button.name));
            button.image.color = Color.green;
            numclicked++;
        }
        else if (numclicked < 5)
        {
            pair3[0] = (whichButton(button.name));
            button.image.color = Color.yellow;
            numclicked++;
        }
        else if (numclicked < 6)
        {
            pair3[1] = (whichButton(button.name));
            button.image.color = Color.yellow;
            numclicked++;
        }
        

    }

    public int whichButton(string name)
    {
        switch (name){
            case "Animal1":
                return 1;
            case "Animal2":
                return 2;
            case "Animal3":
                return 3;
            case "Answer1":
                return 1;
            case "Answer2":
                return 2;
            case "Answer3":
                return 3;
        }
        return 0;
    }

    public int[,] setUpButtons(int[,]answers)
    {
        int[,] allPairs = { { 0,0 } , {0,0 } , {0,0} };

        //choose random buttons 
        List<int> animalButtonChoices = new List<int> { 1, 2, 3 };
        List<int> questionButtonChoices = new List<int> { 1, 2, 3 };

        System.Random random = new System.Random();
        System.Random random2 = new System.Random();


        for (int i = 0; i < 3; i++)
        {
            // Choose a random number from the list
            int randomIndex1 = random.Next(0, animalButtonChoices.Count);
            int randomIndex2 = random2.Next(0, questionButtonChoices.Count);

            int chosenNum1 = animalButtonChoices[randomIndex1];
            int chosenNum2 = questionButtonChoices[randomIndex2];

            animalButtonChoices.RemoveAt(randomIndex1);
            questionButtonChoices.RemoveAt(randomIndex2);

            allPairs[i,0] = chosenNum1;
            allPairs[i,1]= chosenNum2;
           
        }

        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        //set up text for buttons
        for (int i = 0; i < 3; i++)
        {
            int buttonToDo1 = allPairs[i,0];
          
            switch (buttonToDo1)
            {
                case 1:
                    animal1Text.text = data[(answers[i,0] * 12)];
                    break;
                case 2:
                    animal2Text.text = data[(answers[i,0] * 12)];
                    break;
                case 3:
                    animal3Text.text = data[(answers[i,0] * 12)];
                    break;

            }

            int buttonToDo2 = allPairs[i, 1];
            string Question = "";
            //set question
            switch (answers[i,1])
            {
                case 1:
                    Question = "Conservation status: ";
                    break;
                case 2:
                    Question = "Latin name: ";
                    break;
                case 3:
                    Question = "Diet: ";
                    break;
                case 4:
                    Question = "Average lifespan in the WILD: ";
                    break;
                case 5:
                    Question = "Average lifespan in CAPTIVITY: ";
                    break;
                case 6:
                    Question = "Average WEIGHT: ";
                    break;
                case 7:
                    Question = "Average LENGTH: ";
                    break;
                case 8:
                    Question = "Average HEIGHT: ";
                    break;
                case 9:
                    Question = "Average Offspring: ";
                    break;
                case 10:
                    Question = "Predators: ";
                    break;
            }


            switch (buttonToDo2)
            {
                case 1:
                    answer1Text.text = Question  +data[(answers[i, 0] * 12) + answers[i, 1] ];
                    break;
                case 2:
                    answer2Text.text = Question  + data[(answers[i, 0] * 12) + answers[i, 1] ];
                    break;
                case 3:
                    answer3Text.text = Question  + data[( answers[i, 0] * 12) + answers[i, 1]];
                    break;

            }


        }



        //return array of pairs
        return allPairs;
    }


    // Update is called once per frame
    public int[,] setupQuestions()
    {

        //work out what know
        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);
        int[] informationKnow = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        bool isFirstLine = true;
        int x = 1;
        foreach (string line in knowledgeData)
        {
            if (isFirstLine)
            {
                isFirstLine = false;
                continue; // Skip the first line
            }
            string[] values = line.Split(',');
            informationKnow[x] = int.Parse(values[1]);
            x = x + 1;
        }

        int[] animalChosen = { 0, 0, 0 };
        int[] questionChosen = { 0, 0, 0 };
        //choose answers to match
        for (int i = 0; i < 3; i++)
        {
            int[] valuestoAdd = chooseInfo(animalChosen, questionChosen, informationKnow);
            animalChosen[i] = valuestoAdd[0];
            questionChosen[i] = valuestoAdd[1];

        }
        //returns unique set of answers and animals 
        int[,] toReturn = { {animalChosen[0], questionChosen[0] }, { animalChosen[1], questionChosen[1] }, { animalChosen[2], questionChosen[2] }, };
        return toReturn;
    }






    public int[] chooseInfo(int[] animalDone, int[]questionDone, int[]knowledgeHave)
    {
        int animalChose = 0;
        int questionChose = 0;

        bool looping  = true;

        while (looping == true)
        {
            int AnimalToAsk = UnityEngine.Random.Range(1, 19);
            int QuestionToAsk = UnityEngine.Random.Range(1, 11);

            //check is known
            int amountKnown = knowledgeHave[AnimalToAsk];
            bool check1 = false;
            if (amountKnown == 5)
            {
                check1 = true;
            }
            else if (amountKnown == 4 && QuestionToAsk <= 10)
            {
                check1 = true;
            }
            else if (amountKnown == 3 && QuestionToAsk <= 8)
            {
                check1 = true;
            }
            else if (amountKnown == 2 && QuestionToAsk <= 5)
            {
                check1 = true;
            }
            else if (amountKnown == 1 && QuestionToAsk <= 2)
            {
                check1 = true;
            }

            //check animal not done 
            int index = Array.IndexOf(animalDone, AnimalToAsk);
            bool check2 = false;
            if (index == -1)
            {
                check2 = true;
            }


            //check answer to q not done 
            string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);

            string ansToAdd = data[(AnimalToAsk * 12) + QuestionToAsk];
            bool check3 = true;
            
            //answer not applicable to others
            bool check4 = true;

            for (int i = 0; i < 3; i++)
            {
                string currentAns = data[(animalDone[i] * 12) + questionDone[i]];
                string otherAnimalAns = data[(animalDone[i] * 12) + QuestionToAsk];

                if (ansToAdd == currentAns)
                {
                    check3 = false;
                }

                if(ansToAdd == otherAnimalAns)
                {
                    check4 = false;
                }

            }







            if (check1 == true && check2 == true && check3 == true && check4 == true)
            {
                looping = false;
                animalChose = AnimalToAsk;
                questionChose = QuestionToAsk;
            }
        }

        int[] toReturn = { animalChose, questionChose };
        return toReturn;
    }
}
