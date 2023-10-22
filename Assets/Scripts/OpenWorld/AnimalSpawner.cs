using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{

    public GameObject[] animalPrefabs; // Assign your animal prefabs to this array in the Inspector
    public float spawnInterval = 5.0f; // Time between spawns
    public float spawnRadius = 10.0f; // Maximum distance can be spawned at
    public float maxPlayerDistance = 10.0f; // Maximum distance can be spawned from player
    public float minPlayerDistance = 3.0f; // Minimum distance can be spawned from player
    public int maxSpawnedAnimals = 5; // Maximum number of animals that can be spawned at one time

    public Transform player; // Reference to the player object
    private List<GameObject> spawnedAnimals = new List<GameObject>(); //list of animals spawned so far


    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //checks player between right dist and not spawned too many
        if (timer <= 0.0f && distanceToPlayer <= maxPlayerDistance && distanceToPlayer >= minPlayerDistance && spawnedAnimals.Count < maxSpawnedAnimals )
        {
            if (spawnedAnimals.Count >= maxSpawnedAnimals)
            {
                RemoveFurthestAnimal();
            }
            SpawnAnimal();
            timer = spawnInterval;
        }

    }

    private void SpawnAnimal()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 0.0f;

        // Choose a random animal prefab from the array.
        GameObject selectedAnimal = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        GameObject newAnimal = Instantiate(selectedAnimal, randomPosition, Quaternion.identity);
        spawnedAnimals.Add(newAnimal);
    }

    private void RemoveFurthestAnimal()
    {
        float maxDistance = 0f;
        int furthestIndex = -1;

        for (int i = 0; i < spawnedAnimals.Count; i++)
        {
            float distance = Vector3.Distance(spawnedAnimals[i].transform.position, player.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestIndex = i;
            }
        }

        if (furthestIndex >= 0)
        {
            Destroy(spawnedAnimals[furthestIndex]);
            spawnedAnimals.RemoveAt(furthestIndex);
        }
    }

}
