using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float jumpForce = 4f;
    public float sprintSpeedMultiplier = 1.5f;
    public float moveSpeed, startTime;
    private int jetpackUses;
    private float timeForJetpack = 3f;
    private bool jetPackOn = false;
    public Rigidbody2D Rb;

    private Vector2 moveDirection;



  

    // Update is called once per frame
    void Update()
    {
        jetpackUses = PlayerPrefs.GetInt("JetpackUses");
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

        if (Input.GetKey(KeyCode.Space))
        {
            jetPackOn = true;
        }

        if (Input.GetKey(KeyCode.Space) && (jetpackUses > 0) && timeForJetpack > 0 && jetPackOn)
        {
            speedMultiplier = jumpForce;
            timeForJetpack -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && (jetpackUses > 0))
        {
            jetPackOn = false;
            PlayerPrefs.SetInt("JetpackUses", jetpackUses - 1);
            PlayerPrefs.Save();
            timeForJetpack = 3f;
        }


        moveDirection = new Vector2(moveX, moveY).normalized * speedMultiplier;
     
    }

    void Move()
    {
        Rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

}
