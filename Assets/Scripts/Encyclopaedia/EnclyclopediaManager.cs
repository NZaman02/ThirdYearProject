using System;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public GameObject animalButtonPrefab;
    public Transform gridParent;
    public AnimalAns[] animalDataArray;
    public List<AnimalAns> myAnimalFactsList = new List<AnimalAns>();
    public TextAsset answerBankText;

    void Start()
    {
        PopulateAnimalGrid();
    }

    void PopulateAnimalGrid()
    {
        readCSV();
        foreach (var animalData in myAnimalFactsList)
        {
            if (animalData.name != "Animal")
            {
                //sets up all buttons for encylopedia grid from csv
                GameObject buttonInstance = Instantiate(animalButtonPrefab, gridParent);
                AnimalButton animalButton = buttonInstance.GetComponent<AnimalButton>();
                animalButton.SetAnimalData(animalData);
                
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
