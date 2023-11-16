using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class QuestionsAndAnswers : MonoBehaviour 
{
    //save questions
    public string Question;
    public string[] Answers;
    public int CorrectAnswer;


    //read csv stuff
    public TextAsset answerBankText;
    public List<AnimalAns> myAnimalFactsList = new List<AnimalAns>();


    public int setUp()
    {
        Answers = new string[] { "A", "B", "C", "D" };
        readCSV();
        string animalInteracted = PlayerPrefs.GetString("Animal");

        int indexNeeded = 0;
        //finds where animal is that will be asked about 
        for (int i = 0; i < myAnimalFactsList.Count; i++)
        {   
            if (myAnimalFactsList[i].name == animalInteracted)
            {
                indexNeeded = i;
            }
        }
    

        //find 3 non related animals to deceive
        List<int> possibleIndices = Enumerable.Range(0, myAnimalFactsList.Count).ToList();
        possibleIndices.Remove(indexNeeded);
        System.Random random = new System.Random();
        int[] incorrectAnimals = (possibleIndices)
                         .OrderBy(_ => random.Next())
                         .Take(3)
                         .ToArray();

        //set question
        Question = myAnimalFactsList[indexNeeded].name;

        //place right answers and wrong answers 
        CorrectAnswer = UnityEngine.Random.Range(0, 3);
        int wrongAniDone = 0;
        //fills up all 4 answers
        for(int i = 0; i < 4; i++)
        {
            //puts away right answer in correct answer spot
            if(i == CorrectAnswer)
            {
               
                Answers[i] = myAnimalFactsList[indexNeeded].name;
            }
            else
            {
                //puts wrong answers in extra answer spots
                Answers[i] = myAnimalFactsList[incorrectAnimals[wrongAniDone]].name;
                wrongAniDone ++;
            }
        }
        return CorrectAnswer;
    }

    public void readCSV()
    {
        //reads CSV into string
        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12 - 1;

        for (int i = 0; i < numOfAnimal; i++)
        {
                int offset = 12 * i;


            //writes csv into datastructure
            AnimalAns newAnimal = new AnimalAns();

            newAnimal.name = data[offset];
            newAnimal.endangeredStatus = data[offset + 1];
            newAnimal.latinName = data[offset + 2];
            newAnimal.diet = data[offset + 3];
            newAnimal.wildAge = data[offset + 4];
            newAnimal.captivAge = data[offset + 5];
            newAnimal.weight = data[offset + 6];
            newAnimal.anLength = data[offset + 7];
            newAnimal.anheight = data[offset + 8];
            newAnimal.offspringNum = data[offset + 9];
            newAnimal.predators = data[offset + 10];
            newAnimal.funFact = data[offset + 11];

            myAnimalFactsList.Add(newAnimal);
        }

       
    }
}

