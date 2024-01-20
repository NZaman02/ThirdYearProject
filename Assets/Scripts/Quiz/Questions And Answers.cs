using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

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
        Answers = new string[] { "A", "A", "A", "A" };
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
        List<int> possibleIndices = Enumerable.Range(1, myAnimalFactsList.Count).ToList();
        possibleIndices.Remove(indexNeeded);
        System.Random random = new System.Random();
        int[] incorrectAnimals = (possibleIndices)
                         .OrderBy(_ => random.Next())
                         .Take(3)
                         .ToArray();    

        //working out what questions can be asked
        int[] QuestionsAvailable = { 2, 5, 8, 10, 10 };
        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);

        int questionsToPick = 1;
        
        foreach (string line in knowledgeData)
        {
            string[] values = line.Split(',');
            if (values[0] == animalInteracted)
            {
                questionsToPick = int.Parse(values[1]);
                break;
            }
        }
       
        //choses a question to ask from available
        int QuestionToAsk = UnityEngine.Random.Range(1, QuestionsAvailable[questionsToPick - 1]);
  
        //set question
        switch (QuestionToAsk)
        {
            case 1:
                Question = "What is this animal's conservation status";
                break;
            case 2:
                Question = "What is this animal's latin name?";
                break;
            case 3:
                Question = "What type of diet does this animal follow?";
                break;
            case 4:
                Question = "What is the average lifespan for this animal in the WILD?";
                break;
            case 5:
                Question = "What is the average lifespan for this animal in CAPTIVITY?";
                break;
            case 6:
                Question = "What is the average WEIGHT for this animal?";
                break;
            case 7:
                Question = "What is the average LENGTH for this animal?";
                break;
            case 8:
                Question = "What is the average HEIGHT for this animal?";
                break;
            case 9:
                Question = "How many offspring does the animal have on average?";
                break;
            case 10:
                Question = "Who is this animal's predators?";
                break;
        }

        
        string[] myAttributes = { "name", "endangeredStatus", "latinName", "diet", "wildAge", "captivAge", "weight", "anLength", "anheight", "offspringNum", "predators" };

        //place right answers and wrong answers 
        CorrectAnswer = UnityEngine.Random.Range(0, 3);
        int wrongAniDone = 0;
        string attributeName = myAttributes[QuestionToAsk];
        FieldInfo field = typeof(AnimalAns).GetField(attributeName);

        //forces 3 answers boxes for diet q
        string[] incorrectDiet = { "Herbivore", "Omnivore", "Carnivore", "Herbivore", "Omnivore", "Carnivore" };
        List<string> stringList = new List<string>(incorrectDiet);
        if (QuestionToAsk == 3)
        {
            stringList.Remove(myAnimalFactsList[indexNeeded].ToString());
            incorrectDiet = stringList.ToArray();
        }

        //fills up all 4 answers
        for (int i = 0; i < 4; i++)
        {
        //puts away right answer in correct answer spot
            if (i == CorrectAnswer)
            {
                object attributeValue = field.GetValue(myAnimalFactsList[indexNeeded]);
                Answers[i] = attributeValue?.ToString();
           
            }
            else
                {
                if (QuestionToAsk == 3)
                {
                    //if diet situation
                    string correctAns = field.GetValue(myAnimalFactsList[indexNeeded]).ToString();
                    //if not already added/ correct answer so no duplicates
                    if (!(Array.Exists(Answers, element => element == incorrectDiet[wrongAniDone])) && !(string.Equals(incorrectDiet[wrongAniDone], correctAns, StringComparison.Ordinal)))
                    {
                        Answers[i] = incorrectDiet[wrongAniDone];
                    }
                    wrongAniDone++;
                }
                else
                {
                    //puts wrong answers in extra answer spots
                    object attributeValue = field.GetValue(myAnimalFactsList[incorrectAnimals[wrongAniDone]]);
                 

                    //checks not already in or is a duplicate of answer
                    if (!(Answers.Contains(attributeValue.ToString())) && !(attributeValue?.ToString() == field.GetValue(myAnimalFactsList[indexNeeded])?.ToString())   )
                    {
                        Answers[i] = attributeValue?.ToString();
                        wrongAniDone++;
                   

                    }
                    else
                    {
                        //keeps trying to find a false answer to use
                        Answers[i] = wrongAnswersMaker(field.GetValue(myAnimalFactsList[indexNeeded])?.ToString(), Answers, possibleIndices, field);
                        wrongAniDone++;
                    }
                }


            }

        }
        for (int i = 0; i < Answers.Length; i++)
        {
            switch (QuestionToAsk) 
            { 
                case 4:
                    Question = "What is the average lifespan for this animal in the WILD?";
                    Answers[i] = Answers[i].ToString() + " Years";
                    break;
                case 5:
                    Question = "What is the average lifespan for this animal in CAPTIVITY?";
                    Answers[i] = Answers[i].ToString() + " Years";
                    break;
                case 6:
                    Question = "What is the average WEIGHT for this animal?";
                    Answers[i] = Answers[i].ToString() + " Kg";
                    break;
                case 9:
                    Question = "How many offspring does the animal have on average?";
                    Answers[i] = Answers[i].ToString() + " a year";
                    break;
                default:
                    break;
           
            }
        }
      

        return CorrectAnswer;
        
    }

    //need to check what correct ans was, whats already used ,what field is being asked, what animals can be chosen ,what the q is 
    public string wrongAnswersMaker(string correctAnswer, string[] currentlyUsed, List<int> animalChoices, FieldInfo attributeName)
    {
        bool looping = true;
        string ourChoice = "";
        while (looping)
        {
            int randomAnimal = UnityEngine.Random.Range(0, animalChoices.Count);
            ourChoice = (attributeName.GetValue(myAnimalFactsList[randomAnimal]))?.ToString();
            //makes sure wrong ans not already used
          if( !(currentlyUsed.Contains(ourChoice) && !(correctAnswer == ourChoice))) 
            {
                looping = false;
            }

        }
        return ourChoice;
    }

    public void readCSV()
    {
        //reads CSV into string
        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12;

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
