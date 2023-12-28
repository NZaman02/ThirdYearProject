using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float sprintSpeedMultiplier = 1.5f;
    public float moveSpeed;
    public Rigidbody2D Rb;

    private Vector2 moveDirection; 
   
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    //better for physics than update
    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        //only 0 or 1
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprintSpeedMultiplier : 1.0f;


        moveDirection = new Vector2(moveX, moveY).normalized * speedMultiplier; ;
    }

    void Move()
    {
        Rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

}
