using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimalNPCController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float wanderTime = 3.0f;
    public float maxSpeed = 5.0f;
    public float maxDistFromPlayer = 20.0f;
    public string playerTag = "Player";
    private Transform playerTransform;



    private float timer;
    private Vector3 randomDirection;


    // Start is called before the first frame update
    void Start()
    {
        timer = wanderTime;
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform; // Find the player due to prefab
        GetNewRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            GetNewRandomDirection();
            timer = wanderTime;
        }

        // Move the animal in the chosen direction
        Vector3 movement = randomDirection * maxSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Ensure the NPC does not exceed the maximum speed.
        if (movement.magnitude > maxSpeed)
        {
            transform.Translate((movement.normalized * maxSpeed - movement) * Time.deltaTime);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);


        if (distanceToPlayer > maxDistFromPlayer)
        {
            // Destroy the NPC if it's too far from the player.
            Destroy(gameObject);
        }
    }

    private void GetNewRandomDirection()
    {
        // Generate a new random direction for the animal to move
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
