using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float jumpForce = 5f;
    public float bounceForce = 2.5f;
    public float turning = 0f;
    public float turnSpeed = 0.2f;
    public float turnSpeedGround = 0f;
    public float turnPlayerMax = 2f;
    public float turnGroundMax = 0f;
    public bool onGround = false;
    public bool onBounce = false;
    public float preJumpTimer = 0;
    public float preJump = 0.15f;
    public GameObject deathCollider;
    public GameObject scoreController;
    public bool isPaused = false;
    public bool hasLost;

    // Update is called once per frame
    void Update()
    {
        isPaused = scoreController.GetComponent<SpeedController>().isPaused;

        if (Input.GetButtonDown("Jump")&&!isPaused)
        { // When the "Jump" key is pressed (configurable), the the preJumpTimer is started
            // This allows the player to press the button a little bit too early and still be allowed to jump
            preJumpTimer = preJump;
        }

        if(!onGround && turning>=turnGroundMax)
        {   //To avoid the player cheating by keeping the same turning speed on ground as in air (this would make them spin faster),
            //if they are in the air and their turn speed is faster their speed is immediately set to the maximum air speed.
            turning = turnPlayerMax;
        }

        if (Input.GetKey("a") && onGround && turning < turnGroundMax)
        { //This checks if the player is on the ground. If they are they will turn quicker.
            turning += turnSpeedGround;
        }
        else if (Input.GetKey("a") && !onGround && turning < turnPlayerMax)
        { //This checks if the player is in the air. If they are they will turn slower.
            turning += turnSpeed;
        }
        else if (!Input.GetKey("a"))
        { //if the rotation key isn't being pressed there is no additional force added to the player
            turning = 0;
        }

        //This references the script in the deathCollider so it can test whether the player has died
        hasLost = deathCollider.GetComponent<DeathCollisionScript>().hasLost;
    }

    // FixedUpdate is called as several times per frame, this is where I run my physics operations in order for them to not be reliant on framerate
    private void FixedUpdate()
    {
        if (preJumpTimer > 0)
        {
            if (onGround)
            { //Here it checks if the timer is bigger then 0 and the player is on the ground, if so they will jump
                Jump(jumpForce); //this is the Jump() procedure being triggered when the player presses jump button
                preJumpTimer = 0;
            }
            //This counts down the timer when it is active
            preJumpTimer -= Time.deltaTime;
        }

        if (onBounce)
        { //This makes the player "bounce" at the end of an obstacle in case the momentum isn't quite enough to look realistic
            Jump(bounceForce);
        }

        // Here I run AddTorque using the turning variable, this is what rotates the player when the button is pressed
        gameObject.GetComponent<Rigidbody2D>().AddTorque(turning);
    }

    private void Jump(float force)
    { //this Jump() procedure adds upwards force to the player
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, force - gameObject.GetComponent<Rigidbody2D>().velocity.y, 0f), ForceMode2D.Impulse);
    }

            private void OnTriggerStay2D(Collider2D ground)
    { //if the trigger collider (below the wheels) is in contact with the ground then onGround is set to true, allowing the player to jump
        if (ground.CompareTag("Ground")) 
        {
            onGround = true;
        }
        if (ground.CompareTag("Bounce"))
        {
            onBounce = true;
        }
    }
    private void OnTriggerExit2D(Collider2D ground)
    { //This sets onGround to false as soon as player leaves ground
        onGround = false;
        onBounce = false;
    }
}
