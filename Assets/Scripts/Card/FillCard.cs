using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FillCard : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text endangeredStatus;
    public TMP_Text latinName;
    public TMP_Text diet;
    public TMP_Text wildAge;
    public TMP_Text captivAge;
    public TMP_Text weight;
    public TMP_Text anLength;
    public TMP_Text anheight;
    public TMP_Text offspringNum;
    public TMP_Text predators;
    public TMP_Text funFact;
    public Image animalImage;


    public TextAsset answerBankText;
    public TextAsset knowledgeText;

    // Start is called before the first frame update
    void Start()
    {
        string animalName = PlayerPrefs.GetString("Animal");
        int playerKnowledgeLevel = 0;

        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);
        foreach (string line in knowledgeData)
        {
            string[] values = line.Split(',');
            if (values[0] == animalName)
            {
                playerKnowledgeLevel = int.Parse(values[1]);
                break;
            }
        }

        endangeredStatus.text = "<b>Endangered Status: </b>" + "Locked";
        latinName.text = "Locked";
        diet.text = "<b>Dietary Habit: </b>" + "Locked";
        wildAge.text = "<b>Average Lifespan in Wild: </b>" + "Locked";
        captivAge.text = "<b>Average Lifespan in Captivity </b>" + "Locked";
        weight.text = "<b>Average Weight/kg: </b>" + "Locked";
        anLength.text = "<b>Average Length/m: </b>" + "Locked";
        anheight.text = "<b>Average Height/m: </b>" + "Locked";
        offspringNum.text = "<b>Average Offspring: </b>" + "Locked";
        predators.text = "<b>Predators: </b>" + "Locked";
        funFact.text = "Locked";

        //reads data from answer bank csv

        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12 - 1;
        for (int i  = 0; i < numOfAnimal; i++)
        {
            int offset = 12 * i;
            if (data[offset] == animalName)
            {
                //assign relevant info to text once found
                nameText.text =  data[offset];
                if(playerKnowledgeLevel >= 1) {
                    endangeredStatus.text = "<b>Endangered Status: </b>" + data[offset + 1];
                    latinName.text = data[offset + 2];
                }
                if (playerKnowledgeLevel >= 2)
                {
                    diet.text = "<b>Dietary Habit: </b>" + data[offset + 3];
                    wildAge.text = "<b>Average Lifespan in Wild: </b>" + data[offset + 4];
                    captivAge.text = "<b>Average Lifespan in Captivity </b>" + data[offset + 5];
                }
                if (playerKnowledgeLevel >= 3)
                {
                    weight.text = "<b>Average Weight/kg: </b>" + data[offset + 6];
                    anLength.text = "<b>Average Length/m: </b>" + data[offset + 7];
                    anheight.text = "<b>Average Height/m: </b>" + data[offset + 8];
                }
                if (playerKnowledgeLevel >= 4)
                {
                    offspringNum.text = "<b>Average Offspring: </b>" + data[offset + 9] + " a year";
                    predators.text = "<b>Predators: </b>" + data[offset + 10];
                }
                if (playerKnowledgeLevel >= 5)
                {
                    funFact.text = data[offset + 11];
                }

                string imagePath = $"Assets/Sprites/Animals/{data[offset]}.png";
                Texture2D texture = LoadTexture(imagePath);
                //adds image
                if (texture != null)
                {
                    Sprite animalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    animalImage.sprite = animalSprite;
                }
                break;
            }
        }

       



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
            //if couldnt find image just do NA image
            byte[] fileData = System.IO.File.ReadAllBytes($"Assets/Sprites/Animals/NA.png");
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
    }
   
}
