using System;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        //reads data from csv
        string animalName = PlayerPrefs.GetString("Animal");
        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
      
        int numOfAnimal = data.Length / 12 - 1;
        for (int i  = 0; i < numOfAnimal; i++)
        {
            int offset = 12 * i;
            if (data[offset] == animalName)
            {
                //assign relevant info to text once found
                nameText.text =  data[offset];
                endangeredStatus.text = "<b>Endangered Status: </b>" + data[offset + 1];
                latinName.text = data[offset + 2];
                diet.text = "<b>Dietary Habit: </b>" + data[offset + 3];   
                wildAge.text = "<b>Average Lifespan in Wild: </b>" + data[offset + 4];
                captivAge.text = "<b>Average Lifespan in Captivity </b>" + data[offset + 5];
                weight.text = "<b>Average Weight/kg: </b>" + data[offset + 6];
                anLength.text = "<b>Average Length/m: </b>" + data[offset + 7];
                anheight.text = "<b>Average Height/m: </b>" + data[offset + 8];
                offspringNum.text = "<b>Average Offspring: </b>" + data[offset + 9] + " a year";
                predators.text = "<b>Predators: </b>" + data[offset + 10];
                funFact.text = data[offset + 11];
                
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
