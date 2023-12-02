using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;


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

        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);

        foreach (string line in knowledgeData)
        {
            string[] values = line.Split(',');

            if (values[0] == data.name)
            {
                starNum = int.Parse(values[1]);
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
