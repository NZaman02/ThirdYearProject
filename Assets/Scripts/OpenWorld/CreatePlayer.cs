using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatePlayer : MonoBehaviour
{
    public GameObject player;
    public string currentBiome;
    private CameraFollow cameraFollow;
    public GameObject cameraObject;


    // Start is called before the first frame update
    void Start()
    {
        CameraFollow scriptInstance = cameraObject.AddComponent<CameraFollow>();
        spawnPlayer(currentBiome);
    }

    private void spawnPlayer(string theCurrentBiome)
    {

        switch (theCurrentBiome)
        {
            case "Woodlands":
                Vector3 spawnPosition = new Vector3(70.0f, 4.0f, 1f);
                GameObject spawnedPrefab = Instantiate(player, spawnPosition, Quaternion.identity);
                break;
            case "Grasslands":
                break;
        }
      
    }
    
}
