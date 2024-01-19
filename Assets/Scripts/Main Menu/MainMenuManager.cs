using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenuManager : MonoBehaviour
{


    public Button newGameButton;
    public Button loadGameButton;
    public Button quitGameButton, exportDataButton;
    public InputField pathInputField;
    public TextAsset answerBankText;
    public string exportPath;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(NewGame);
        loadGameButton.onClick.AddListener(LoadGame);
        quitGameButton.onClick.AddListener(QuitGame);
        exportDataButton.onClick.AddListener(exportData);
        pathInputField.onEndEdit.AddListener(ReadInput);

    }

    void NewGame()
    {
        //resets player knowledge
        string[] data = answerBankText.text.Split(new[] { ",", "\n" }, StringSplitOptions.None);
        int numOfAnimal = data.Length / 12 - 1;

        List<string> animalNames = new List<string>();

        for (int i = 0; i < numOfAnimal; i++)
        {
            int offset = 12 * i;
            animalNames.Add(data[offset]);
        }

        string filePath = Path.Combine(Application.persistentDataPath, "playerKnowledge.csv");

        TextWriter tw = new StreamWriter(filePath, false);
        for (int i = 0; i < animalNames.Count; i++)
        {
            tw.WriteLine(animalNames[i] + ",0");
        }
        tw.Close();

        filePath = Path.Combine(Application.persistentDataPath, "playerStats.csv");
        tw = new StreamWriter(filePath, false);
        tw.WriteLine("WhichQ,GotCorrect,TimeTaken");



        LoadGame();
    }


    void LoadGame()
    {
        SceneManager.LoadScene("Grasslands");

    }


    private void ReadInput(string input)
    {
        exportPath = input;
    }

    private void exportData()
    {
        if (!string.IsNullOrEmpty(exportPath))
        {
            string content = "";
            using (StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, "playerStats.csv")))
            {
                // Read the entire content of the CSV file
                content = reader.ReadToEnd();
            }

            StreamWriter writer = new StreamWriter(exportPath, true); // 'true' means append, 'false' means overwrite

            // Write the data to the file
            writer.WriteLine(content);

            // Close the file
            writer.Close();
        }

    }

    void QuitGame()
    {
        Application.Quit();

    }

}

      

