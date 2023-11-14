using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class QuizManager : MonoBehaviour
{
    //showing question stuff
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public TMP_Text QuestionTxt;

    //read csv stuff
    public TextAsset answerBankText;

    //helps choose animals 
    public string animalInteracted;
    public List<AnimalAns> myAnimalFactsList = new List<AnimalAns>();  
            
    private void Start()
    {
        readCSV();
        generateQuestion();
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);

        generateQuestion();
    }


    void setAnswers()
    {
        //will get an answer from saved csv stuff
        for (int i = 0; i < options.Length; i++) {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent <AnswerScript>().isCorrect = true;
            }
        }
        
        //finds where 
        for(int i = 0; i < myAnimalFactsList.length; i++)
        {

        }

    }

    void generateQuestion()
    {
        //chooses random question
        if (QnA.Count > 0)
        {
            currentQuestion = UnityEngine.Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            setAnswers();
        }
        else
        {
            SceneManager.LoadScene("Open World Scene");
        }
       

    }

    void readCSV()
    {
        //reads CSV into string
        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12 - 1;
   
       
        for(int i = 0; i < numOfAnimal; i++)   
        {
            //writes csv into datastructure
            AnimalAns newAnimal = new AnimalAns();
            
            newAnimal.name = data[4 * (i + 1)];
            newAnimal.endangeredStatus = data[4 * (i + 1) + 1];
            newAnimal.latinName = data[4 * (i + 1) + 1];
            newAnimal.diet = data[4 * (i + 1) + 1];
            newAnimal.wildAge = data[4 * (i + 1) + 1];
            newAnimal.captivAge = data[4 * (i + 1) + 1];
            newAnimal.weight = data[4 * (i + 1) + 1]  ;
            newAnimal.anLength = data[4 * (i + 1) + 1];
            newAnimal.anheight = data[4 * (i + 1) + 1];
            newAnimal.offspringNum = data[4 * (i + 1) + 1];
            newAnimal.predators = data[4 * (i + 1) + 1];
            newAnimal.funFact = data[4 * (i + 1) + 1];

            myAnimalFactsList.Add(newAnimal);
}       


    }


}
