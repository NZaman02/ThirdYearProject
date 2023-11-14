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

    public string animalInteracted;
    public List<AnimalAns> myAnimalFactsList = new List<AnimalAns>();


    public void setUpQA()
    {
        readCSV();
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


        //place right answers and wrong q away
        CorrectAnswer = UnityEngine.Random.Range(0, 3);
        int wrongAniDone = 0;
        //fills up all 4 answers
        for(int i = 0; i < 3; i++)
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

    }

    public void readCSV()
    {
        //reads CSV into string
        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12 - 1;


        for (int i = 0; i < numOfAnimal; i++)
        {
            //writes csv into datastructure
            AnimalAns newAnimal = new AnimalAns();

            newAnimal.name = data[4 * (i + 1)];
            newAnimal.endangeredStatus = data[4 * (i + 1) + 1];
            newAnimal.latinName = data[4 * (i + 1) + 1];
            newAnimal.diet = data[4 * (i + 1) + 1];
            newAnimal.wildAge = data[4 * (i + 1) + 1];
            newAnimal.captivAge = data[4 * (i + 1) + 1];
            newAnimal.weight = data[4 * (i + 1) + 1];
            newAnimal.anLength = data[4 * (i + 1) + 1];
            newAnimal.anheight = data[4 * (i + 1) + 1];
            newAnimal.offspringNum = data[4 * (i + 1) + 1];
            newAnimal.predators = data[4 * (i + 1) + 1];
            newAnimal.funFact = data[4 * (i + 1) + 1];

            myAnimalFactsList.Add(newAnimal);
        }
    }
}

