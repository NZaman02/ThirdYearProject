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
    public float fadeDuration = 4.5f;


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

        endangeredStatus.text = "Locked";
        latinName.text = "Locked";
        diet.text = "Locked";
        wildAge.text = "Locked";
        captivAge.text = "Locked";
        weight.text = "Locked";
        anLength.text = "Locked";
        anheight.text = "Locked";
        offspringNum.text ="Locked";
        predators.text = "Locked";
        funFact.text = "Locked";

        //reads data from answer bank csv

        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12;
        for (int i  = 0; i < numOfAnimal; i++)
        {
            int offset = 12 * i;
            if (data[offset] == animalName)
            {
                //assign relevant info to text once found
                nameText.text =  data[offset];
                if(playerKnowledgeLevel >= 1) {
                    endangeredStatus.text = data[offset + 1];
                    latinName.text = data[offset + 2];
                }
                if (playerKnowledgeLevel >= 2)
                {
                    diet.text = data[offset + 3];
                    wildAge.text = data[offset + 4];
                    captivAge.text = data[offset + 5];
                }
                if (playerKnowledgeLevel >= 3)
                {
                    weight.text =data[offset + 6];
                    anLength.text = data[offset + 7];
                    anheight.text = data[offset + 8];
                }
                if (playerKnowledgeLevel >= 4)
                {
                    offspringNum.text = data[offset + 9] + " years";
                    predators.text = data[offset + 10];
                }
                if (playerKnowledgeLevel >= 5)
                {
                    funFact.text = data[offset + 11];
                }
                
                
                Sprite animalSprite = Resources.Load<Sprite>(animalName);

                if (animalSprite != null)
                {
                    animalImage.sprite = animalSprite;
                }
                break;
            }
        }
        //alpha 0 for text to be faded
        if (PlayerPrefs.GetString("JustCaught") == "True")
        {
            //alpha relevant text
            PlayerPrefs.SetString("JustCaught", "False");
            if (playerKnowledgeLevel == 1)
            {
                endangeredStatus.color = new Color(endangeredStatus.color.r, endangeredStatus.color.g, endangeredStatus.color.b, 0f);
                latinName.color = new Color(latinName.color.r, latinName.color.g, latinName.color.b, 0f);
                 StartCoroutine(FadeText(endangeredStatus, endangeredStatus.color.a, 1f, fadeDuration));
                StartCoroutine(FadeText(latinName, latinName.color.a, 1f, fadeDuration));
                
            }
            if (playerKnowledgeLevel== 2)
            {
                diet.color = new Color(diet.color.r, diet.color.g, diet.color.b, 0f);
                wildAge.color = new Color(wildAge.color.r, wildAge.color.g, wildAge.color.b, 0f);
                captivAge.color = new Color(captivAge.color.r, captivAge.color.g, captivAge.color.b, 0f);
                StartCoroutine(FadeText(diet, diet.color.a, 1f, fadeDuration));
                StartCoroutine(FadeText(wildAge, wildAge.color.a, 1f, fadeDuration));
                StartCoroutine(FadeText(captivAge, captivAge.color.a, 1f, fadeDuration));
            }
            if (playerKnowledgeLevel == 3)
            {
                weight.color = new Color(weight.color.r, weight.color.g, weight.color.b, 0f);
                anLength.color = new Color(anLength.color.r, anLength.color.g, anLength .color.b, 0f);
                anheight.color = new Color(anheight.color.r, anheight.color.g, anheight.color.b, 0f);
                StartCoroutine(FadeText(weight, weight.color.a, 1f, fadeDuration));
                StartCoroutine(FadeText(anLength, anLength.color.a, 1f, fadeDuration));
                StartCoroutine(FadeText(anheight, anheight.color.a, 1f, fadeDuration));
            }
            if (playerKnowledgeLevel == 4)
            {
                offspringNum.color = new Color(offspringNum.color.r, offspringNum.color.g, offspringNum.color.b, 0f);
                predators.color = new Color(predators.color.r, predators.color.g, predators.color.b, 0f);
                StartCoroutine(FadeText(offspringNum, offspringNum.color.a, 1f, fadeDuration));
                StartCoroutine(FadeText(predators, predators.color.a, 1f, fadeDuration));
            }
            if (playerKnowledgeLevel == 5)
            {
                funFact.color = new Color(funFact.color.r, funFact.color.g, funFact.color.b, 0f);
                StartCoroutine(FadeText(funFact, funFact.color.a, 1f, fadeDuration));
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

    private IEnumerator FadeText(TMP_Text text, float start, float end, float duration)
    {
       

        float elapsedTime = 0f;
        //fades alpha over time
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            text.color = new Color(1f, 0.92f, 0.016f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

       
        text.color = new Color(1f, 0.92f, 0.016f, end);


    }

}
