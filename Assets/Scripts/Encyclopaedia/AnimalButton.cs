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

        Sprite animalSprite = Resources.Load<Sprite>(data.name);

        if (animalSprite != null)
        {
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

    void OnButtonClick()
    {
        //so quiz knows what animal
        PlayerPrefs.SetString("Animal", animalData.name);
        //activates quiz
        SceneManager.LoadScene("AnimalCard");
    }
}
