using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatePlayer : MonoBehaviour
{
    public string currentBiome;
    private CameraFollow cameraFollow;
    public GameObject cameraObject, playerPrefab;
    private string prevScene;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlayer(currentBiome);
    }

    public void spawnPlayer(string theCurrentBiome)
    {
        GameObject player = GameObject.FindWithTag("Player");
        prevScene = PlayerPrefs.GetString("CameFrom");

        switch (theCurrentBiome)
        {
            case "Woodlands":
                switch  (prevScene)
                {
                    case "Desert": 
                        Vector3 spawnPosition = new Vector3(-2f, 15.0f, 1f);
                        player.transform.position = spawnPosition; 
                        break;
                    case "Savanna":
                        spawnPosition = new Vector3(60f, 20.0f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Grasslands":
                        Debug.Log(player.transform.position);
                        spawnPosition = new Vector3(73f, 2.0f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Coast":
                        spawnPosition = new Vector3(2f, 35.0f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Forest":
                        spawnPosition = new Vector3(-30f, -15.0f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                }
                break;
              
            case "Grasslands":
                switch (prevScene)
                {
                    case "Savanna":
                        Vector3 spawnPosition = new Vector3(40f, 30f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Coast":
                        spawnPosition = new Vector3(6f, -21f, 1f);
                         player.transform.position = spawnPosition;
                        break;
                    case "Woodlands":
                        Debug.Log("A");
                        spawnPosition = new Vector3(-30f, 15f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                }
                break;
            
            case "Savanna":
                switch (prevScene)
                {
                    case "Desert":
                        Vector3 spawnPosition = new Vector3(-12f, 7f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Grasslands":
                        spawnPosition = new Vector3(31f, -4f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Coast":
                        spawnPosition = new Vector3(60f, -50f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                }
                break;
            
            case "Desert":
                switch (prevScene)
                {
                    case "Forest":
                        Vector3 spawnPosition = new Vector3(-8f, -12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Woodlands":
                        spawnPosition = new Vector3(34f, -12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Savanna":
                        spawnPosition = new Vector3(55f, -12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                }
                break;
            
            case "Coast":
                switch (prevScene)
                {
                    case "Forest":
                        Vector3 spawnPosition = new Vector3(-12f, 12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Woodlands":
                        spawnPosition = new Vector3(4.5f, 12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Grasslands":
                        spawnPosition = new Vector3(34f, 12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Savanna":
                        spawnPosition = new Vector3(51f, 12f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                }
                break;
            
            case "Forest":
                switch (prevScene)
                {
                    case "Desert":
                        Vector3 spawnPosition = new Vector3(10f, -1f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Woodlands":
                        spawnPosition = new Vector3(20f, -24f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                    case "Coast":
                        spawnPosition = new Vector3(15f, -24f, 1f);
                        player.transform.position = spawnPosition;
                        break;
                }
                break;
        }
      
    }
    
}
