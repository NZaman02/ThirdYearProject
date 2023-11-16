using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AnimalButton : MonoBehaviour
{
   

    private AnimalAns[] allAnimals;
    private AnimalAns animalData;

    public TMP_Text nameText; // Reference to the Text component displaying the animal's name

    public void SetAnimalData(AnimalAns data)
    {
        animalData = data;
        nameText.text = data.name;
    }

    public void OnButtonClick()
    {
        // Implement logic to show more information about the clicked animal
        Debug.Log($"Clicked on {animalData.name}. Description: {animalData.endangeredStatus}");
    }
}
