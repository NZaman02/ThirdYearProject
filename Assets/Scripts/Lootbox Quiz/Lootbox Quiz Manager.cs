using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class LootboxQuizManager : MonoBehaviour
{

    public TextAsset answerBankText;
    // Start is called before the first frame update
    void Start()
    {
        setupQuestions();
    }

    // Update is called once per frame
    void setupQuestions()
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
