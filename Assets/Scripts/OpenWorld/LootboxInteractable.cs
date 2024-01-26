using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootboxInteractable : MonoBehaviour
{
    public void Interact()
    {

        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");
        string[] knowledgeData = File.ReadAllLines(filePath);
        int totalKnowledgeUnlocked = 0;
        bool isFirstLine = true;
        foreach (string line in knowledgeData)
        {
            if (isFirstLine)
            {
                isFirstLine = false;
                continue; // Skip the first line
            }
            string[] values = line.Split(',');
            totalKnowledgeUnlocked += int.Parse(values[1]);
          
        }
        //make sure know enough before doing
        if (totalKnowledgeUnlocked > 12)
        {
            SceneManager.LoadScene("Lootbox Quiz Scene");
        }
        else
        {
            Debug.Log("Not unlocked enough");
        }

    }
}
