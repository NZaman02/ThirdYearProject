using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
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
    public TextAsset playerKnowledgeText;

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


        //working out what questions can be asked
        int[] QuestionsAvailable = { 2, 5, 8, 10 };
        string[] knowledgeData = playerKnowledgeText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal1 = knowledgeData.Length;
        int questionsToPick = 1;
        for (int i = 0; i < numOfAnimal1; i += 2)
        {
            if (knowledgeData[i] == animalInteracted)
            {
                questionsToPick = int.Parse(knowledgeData[i + 1]);
                break;
            }
        }

        //choses a question to ask from available
        int QuestionToAsk = UnityEngine.Random.Range(1, QuestionsAvailable[questionsToPick-1]);
        
        //set question
        switch (QuestionToAsk)
        {
            case 1: Question = "What is this animal's conservation status";
                break;
            case 2: Question = "What is this animal's latin name?";
                break;
            case 3: Question = "What type of diet does this animal follow?";
                break;
            case 4: Question = "What is the average lifespan for this animal in the WILD?";
                break;
            case 5: Question = "What is the average lifespan for this animal in CAPTIVITY?";
                break;
            case 6: Question = "What is the average WEIGHT for this animal?";
                break;
            case 7: Question = "What is the average LENGTH for this animal?";
                break; 
            case 8: Question = "What is the average HEIGHT for this animal?"; 
                break;
            case 9: Question = "How many offspring does the animal have on average?";
                break;  
            case 10: Question = "Who is this animal's predators?";
                break;
        }

        string[] myAttributes = { "name", "endangeredStatus", "latinName", "diet", "wildAge", "captivAge", "weight", "anLength", "anheight", "offspringNum", "predators" };

        //place right answers and wrong answers 
        CorrectAnswer = UnityEngine.Random.Range(0, 3);
        int wrongAniDone = 0;
        string attributeName = myAttributes[QuestionToAsk];
        FieldInfo field = typeof(AnimalAns).GetField(attributeName);
        Debug.Log(attributeName);
        Debug.Log(field);

        //fills up all 4 answers
        for (int i = 0; i < 4; i++)
        {    
            //puts away right answer in correct answer spot
            if (i == CorrectAnswer)
            {
                object attributeValue = field.GetValue(myAnimalFactsList[indexNeeded]);
                Debug.Log(attributeValue);  
                Answers[i] = attributeValue?.ToString();
            }
            else
            {
                //puts wrong answers in extra answer spots
                object attributeValue = field.GetValue(myAnimalFactsList[incorrectAnimals[wrongAniDone]]);
                Answers[i] = attributeValue?.ToString();
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

