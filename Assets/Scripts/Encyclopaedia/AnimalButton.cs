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
    public TMP_Text nameText; // Reference to the Text component displaying the animal's name


    public void SetAnimalData(AnimalAns data)
    {
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
      
        GetComponent<Button>().onClick.AddListener(OnButtonClick);


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
