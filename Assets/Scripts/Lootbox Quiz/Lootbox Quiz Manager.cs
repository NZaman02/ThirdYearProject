using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class LootboxQuizManager : MonoBehaviour
{

    public TextAsset answerBankText;

    public Button animal1,animal2, animal3, answer1, answer2, answer3, submit, returnOpenWorld;
    public TMP_Text animal1Text, animal2Text, animal3Text, answer1Text, answer2Text, answer3Text;

    private int[] pair1,pair2,pair3;
    private int numclicked;
    private int[,] answersToUse,ansPairs;


    // Start is called before the first frame update
    void Start()
    {
        pair1 = new int[] { 0, 0 };
        pair2 = new int[] { 0, 0 };
        pair3 = new int[] { 0, 0 };

        numclicked = 0;

        answersToUse = setupQuestions();
        ansPairs = setUpButtons(answersToUse);

        animal1.onClick.AddListener(() => addClickedButton(animal1));
        animal2.onClick.AddListener(() => addClickedButton(animal2));
        animal3.onClick.AddListener(() => addClickedButton(animal3));
        answer1.onClick.AddListener(() => addClickedButton(answer1));
        answer2.onClick.AddListener(() => addClickedButton(answer2));
        answer3.onClick.AddListener(() => addClickedButton(answer3));

    }

    private void checkAns()
    {
        int[] pair4 = new int[] { ansPairs[0,0], ansPairs[0,1] };
        int[] pair5 = new int[] { ansPairs[1, 0], ansPairs[1, 1] };
        int[] pair6 = new int[] { ansPairs[2, 0], ansPairs[2, 1] };



        int[][] pairs = {pair1,pair2,pair3, pair4, pair5, pair6 };


        HashSet<HashSet<int>> pairSets = new HashSet<HashSet<int>>(pairs.Select(pair => new HashSet<int>(pair)));

        Debug.Log(pairSets.Count == pairs.Length);

    }

    void Update()
    {
        if(numclicked == 6  )
        {
            checkAns();
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
            case "animal1":
                return 1;
            case "animal2":
                return 2;
            case "animal3":
                return 3;
            case "answer1":
                return 1;
            case "answer2":
                return 2;
            case "answer3":
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


        Debug.Log("Button pairs");
        Debug.Log(allPairs[0, 0].ToString() + " " + allPairs[0, 1].ToString());
        Debug.Log(allPairs[1, 0].ToString() + " " + allPairs[1, 1].ToString());
        Debug.Log(allPairs[2, 0].ToString() + " " + allPairs[2, 1].ToString());

        Debug.Log("Animals");

        Debug.Log(answers[0, 0].ToString() + " " + answers[0, 1].ToString());
        Debug.Log(answers[1, 0].ToString() + " " + answers[1, 1].ToString());
        Debug.Log(answers[2, 0].ToString() + " " + answers[2, 1].ToString());


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
                    Question = "Cnservation status";
                    break;
                case 2:
                    Question = "Latin name?";
                    break;
                case 3:
                    Question = "Diet ";
                    break;
                case 4:
                    Question = "Average lifespan in the WILD?";
                    break;
                case 5:
                    Question = "Average lifespan in CAPTIVITY?";
                    break;
                case 6:
                    Question = "Average WEIGHT";
                    break;
                case 7:
                    Question = "Average LENGTH ";
                    break;
                case 8:
                    Question = "Average HEIGHT";
                    break;
                case 9:
                    Question = "Average Offspring?";
                    break;
                case 10:
                    Question = "Predators";
                    break;
            }


            switch (buttonToDo2)
            {
                case 1:
                    answer1Text.text = Question + ": " +data[(answers[i, 0] * 12) + answers[i, 1] ];
                    break;
                case 2:
                    answer2Text.text = Question + ": " + data[(answers[i, 0] * 12) + answers[i, 1] ];
                    break;
                case 3:
                    answer3Text.text = Question + ": " + data[( answers[i, 0] * 12) + answers[i, 1]];
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
            int numOfAnimal = data.Length / 12;

            string ansToAdd = data[(AnimalToAsk * 12) + QuestionToAsk];
            bool check3 = true;
            for (int i = 0; i < 3; i++)
            {
                string currentAns = data[(animalDone[i] * 12) + questionDone[i]];

                if (ansToAdd == currentAns)
                {
                    check3 = false;
                }

            }

            if (check1 == true && check2 == true && check3 == true)
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
