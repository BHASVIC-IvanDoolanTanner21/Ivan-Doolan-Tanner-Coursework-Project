using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float jumpForce = 5f;
    public bool jumpFlag = false;
    public float turning = 0f;
    public float turnSpeed = 0.2f;
    public float turnPlayerMax = 2f;
    public float turnMax = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        { // When the "Jump" key is pressed (configurable), the jumpFlag is triggered
            jumpFlag = true;
        }
        if (Input.GetKey("a") && turning < turnPlayerMax)
        {
            turning += turnSpeed;
        }
        /*else if (!Input.GetKey("a") && turning > turnMax)
        {
            turning -= turnSpeed;
        }*/
        else if (!Input.GetKey("a"))
        {
            turning = 0;
        }
    }

    // FixedUpdate is called as several times per frame, this is where I run my physics operations in order for them to not be reliant on framerate
    private void FixedUpdate()
    {
        if (jumpFlag == true)
        { //The reason I use a flag is so that I can run the Jump function in FixedUpdate
            Jump(jumpForce); //this is the Jump() procedure being triggered when the player presses jump button
            jumpFlag = false;
        }
        gameObject.GetComponent<Rigidbody2D>().AddTorque(turning);
    }

    private void Jump(float force)
    { //this Jump() procedure adds upwards force to the player
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f,force-gameObject.GetComponent<Rigidbody2D>().velocity.y,0f),ForceMode2D.Impulse);
    }
}
