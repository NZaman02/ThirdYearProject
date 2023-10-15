using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimalNPCController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float wanderTime = 3.0f;

    private float timer;
    private Vector3 randomDirection;


    // Start is called before the first frame update
    void Start()
    {
        timer = wanderTime;
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
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
    }

    private void GetNewRandomDirection()
    {
        // Generate a new random direction for the animal to move
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
