using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class AnimalButton : MonoBehaviour
{
    private AnimalAns[] allAnimals;
    private AnimalAns animalData;
    public Image animalImage;
    public TMP_Text nameText;
    public TextAsset playerKnowledge;
    public Transform gridParent;
    public GameObject starImage;
    private int starNum = 0;

    public void SetAnimalData(AnimalAns data)
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        //sets up images, data and listeners for all buttons
        animalData = data;
        nameText.text = data.name;

        string imagePath = $"Assets/Sprites/Animals/{data.name}.png";
        Texture2D texture = LoadTexture(imagePath);

        if (texture != null)
        {
            Sprite animalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            animalImage.sprite = animalSprite;
        }

        string[] knowledgeData = playerKnowledge.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = knowledgeData.Length;
        for (int i = 0; i < numOfAnimal; i += 2)
        {
            if (knowledgeData[i] == data.name)
            {
                starNum = int.Parse(knowledgeData[i + 1]);         
                break;
            }
        }
     
        if (starNum > 0)
        {
            for (int i = 0;i<starNum; i++)
                {
                    GameObject buttonInstance = Instantiate(starImage.gameObject, gridParent);
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
            byte[] fileData = System.IO.File.ReadAllBytes($"Assets/Sprites/Animals/NA.png");
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
    }

    void OnButtonClick()
    {
        //so quiz knows what animal
        PlayerPrefs.SetString("Animal", animalData.name);
        //activates quiz
        SceneManager.LoadScene("AnimalCard");
    }
}
